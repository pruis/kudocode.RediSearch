using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;

namespace KudoCode.Services.Scheduler.Quartz.Schedules
{
    public class TestSchedule : ISchedule
    {
        public string JobId => "Test";
        public string Cron => "*/2 * * ? * *";
    }
}