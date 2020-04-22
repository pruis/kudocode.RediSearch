using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;

namespace KudoCode.Services.Scheduler.Quartz.Schedules
{
    //At second :00, every 10 minutes starting at minute :00, of every hour
    public class EveryTenMinSchedule : ISchedule
    {
        public string JobId => "EveryTenMin";
        public string Cron => "0 0/10 * ? * * *";
    }
}