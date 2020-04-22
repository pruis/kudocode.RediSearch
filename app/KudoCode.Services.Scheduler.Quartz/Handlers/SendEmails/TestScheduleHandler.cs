using KudoCode.Services.Scheduler.Quartz.Infrastructure;
using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;
using KudoCode.Services.Scheduler.Quartz.Schedules;

namespace KudoCode.Services.Scheduler.Quartz.Handlers.SendEmails
{
    public class TestScheduleHandler : ScheduleHandler, IScheduleHandler<EveryMinSchedule>
    {
        protected override void Action()
        {
            //Debugger.Break();
        }
    }
}