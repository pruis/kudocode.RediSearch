using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;

namespace KudoCode.Services.Scheduler.Quartz.Schedules
{
    //https://www.freeformatter.com/cron-expression-generator-quartz.html#
    public class EverySixAm : ISchedule
    {
        public string JobId => "EverySixAm";
        public string Cron => "0 0 5 ? * * *";
    }
}