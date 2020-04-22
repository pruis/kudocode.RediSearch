namespace KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces
{
    public interface ISchedule
    {
        string JobId { get; }
        string Cron { get; }
    }
}