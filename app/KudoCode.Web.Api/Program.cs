using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace KudoCode.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureLogging(config => { config.ClearProviders(); })
                .UseStartup<Startup>()
                .Build();
    }
}