using KudoCode.Services.Scheduler.Quartz.Infrastructure;
using KudoCode.Services.Scheduler.Quartz.Infrastructure.Interfaces;
using KudoCode.Services.Scheduler.Quartz.Schedules;
using KudoCode.Web.Api.Connector;

namespace KudoCode.Services.Scheduler.Quartz.Handlers.SendEmails
{
    public class SendEmailScheduleHandler : ScheduleHandler
            , IScheduleHandler<EveryMinSchedule>
        //, IScheduleHandler<TestSchedule>
    {
        private readonly Connector _connector;

        public SendEmailScheduleHandler(Connector connector)
        {
            _connector = connector;
        }

        protected override void Action()
        {
            //var response = _connector.EndPoint.Request<SendEmailDto, int>(new SendEmailDto() {ApplicationUserId = 1});
            //var i = response.Result;
        }
    }
}