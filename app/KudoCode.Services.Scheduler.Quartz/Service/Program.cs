using KudoCode.Services.Scheduler.Quartz.Infrastructure;
using Topshelf;
using Topshelf.Autofac;

namespace KudoCode.Services.Scheduler.Quartz.Service
{
    internal static class Program
    {
        static void Main()
        {
            ApplicationContext.Container = ContainerInstaller.BuildContainer();
            var rc = HostFactory.Run(x =>
            {
                x.UseAutofacContainer(ApplicationContext.Container);

                x.Service<SchedulerService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
            });
        }
    }
}