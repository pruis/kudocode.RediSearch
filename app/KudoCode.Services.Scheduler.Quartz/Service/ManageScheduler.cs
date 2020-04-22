using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;
using Quartz;

namespace KudoCode.Services.Scheduler.Quartz.Service
{
    public class ManageScheduler
    {
        private readonly IScheduler _scheduler;

        public ManageScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            _scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Shutdown()
        {
            _scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void AddJob(IJob job, ISchedule schedule)
        {
            ScheduleJobWithCronSchedule(job, schedule);
        }

        private void ScheduleJobWithCronSchedule(IJob job, ISchedule schedule)
        {
            var jobName = $"{job.GetType().Name}-{schedule.JobId}";
            var j = JobBuilder
                .Create(job.GetType())
                .WithIdentity(jobName, $"{jobName}-Group")
                .Build();

            var cronTrigger = TriggerBuilder
                .Create()
                .WithIdentity($"{jobName}-Trigger")
                .StartNow()
                .WithCronSchedule(schedule.Cron)
                .ForJob(j)
                .Build();

            _scheduler.ScheduleJob(j, cronTrigger);
        }
    }
}