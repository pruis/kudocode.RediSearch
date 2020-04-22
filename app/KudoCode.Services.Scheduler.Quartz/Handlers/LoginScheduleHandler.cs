using System;
using System.Linq;
using System.Threading;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Services.Scheduler.Quartz.Infrastructure;
using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;
using KudoCode.Services.Scheduler.Quartz.Schedules;
using KudoCode.Web.Api.Connector;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace KudoCode.Services.Scheduler.Quartz.Handlers
{
    /// <summary>
    /// Refresh the token for the scheduler every day 6am
    /// </summary>
    public class LoginScheduleHandler : ScheduleHandler
        , IScheduleHandler<EverySixAm>
    {
        private readonly IScheduler _scheduler;
        private readonly IConfiguration _configuration;

        public LoginScheduleHandler(IScheduler scheduler, IConfiguration configuration)
        {
            _scheduler = scheduler;
            _configuration = configuration;
        }

        protected override void Action()
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
        }
    }
}