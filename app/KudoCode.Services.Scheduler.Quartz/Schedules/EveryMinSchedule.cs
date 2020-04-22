using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;

namespace KudoCode.Services.Scheduler.Quartz.Schedules
{
    //At second :00 of every minute
    public class EveryMinSchedule : ISchedule
    {
        public string JobId => "SendEmail";
        public string Cron => "0 * * ? * * *";
    }
}