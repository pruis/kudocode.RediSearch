using Core.Services.Workflow.RabbitMQ.Infrastructure;
using Newtonsoft.Json;
using Topshelf;

namespace Core.Services.Workflow.RabbitMQ
{
	class Program
	{
		static void Main(string[] args)
		{
			JsonConvert.DefaultSettings = () => KudoCode.Helpers.Json.Serialization.Objects();

			HostFactory.Run(x =>
			{
				x.Service<WorkFlowService>(s =>
				{
					s.ConstructUsing(name => new WorkFlowService());
					s.WhenStarted(tc => tc.Start());
					s.WhenStopped(tc => tc.Stop());
				});
				x.RunAsLocalSystem();
				x.StartAutomaticallyDelayed();
				x.SetDescription("Internal-WorkFlow");
				x.SetDisplayName("Internal-WorkFlow");
				x.SetServiceName("Internal-WorkFlow");
			}); ;
		}
	}
}
