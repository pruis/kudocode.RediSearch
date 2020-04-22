using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Services.Scheduler.Quartz.Infrastructure;
using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;
using KudoCode.Web.Api.Connector;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace KudoCode.Services.Scheduler.Quartz.Service
{
    public class SchedulerService
    {
        private readonly ManageScheduler _scheduler;
        private readonly IConfiguration _configuration;

        public SchedulerService(ManageScheduler scheduler, IConfiguration configuration)
        {
            _scheduler = scheduler;
            _configuration = configuration;
        }

        public void Start()
        {
            var loggedIn = false;
            while (!loggedIn)
            {
                try
                {
                    var loginDetailsConfig = _configuration.GetSection("loginDetails");

                    var response = ApplicationContext.Container.Resolve<Connector>().Authentication
                        .GetToken(new GetTokenRequest()
                            {Email = loginDetailsConfig["email"], Password = loginDetailsConfig["password"]});

                    if (!response.Messages.Any())
                        loggedIn = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(5000);
                }
            }

            ScheduleJobs();
            _scheduler.Start();
        }

        private void ScheduleJobs()
        {
            var schedules = ApplicationContext.Container.Resolve<IEnumerable<ISchedule>>();

            foreach (var schedule in schedules)
                try
                {
                    var eventHandlers = ((IEnumerable) ApplicationContext.Container.Resolve(
                        typeof(IEnumerable<>).MakeGenericType(typeof(IScheduleHandler<>).MakeGenericType
                            (schedule.GetType()))));

                    foreach (var handler in eventHandlers)
                        _scheduler.AddJob(((IJob) handler), schedule);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        public void Stop()
        {
            _scheduler.Shutdown();
            if (ApplicationContext.Container != null)
                ApplicationContext.Container.Dispose();
        }
    }
}