using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorMobile.Common.Services;
using BlazorMobile.Common;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Mobile.Blazor.Helpers;
using KudoCode.Mobile.Infrastructure.Blazor;
using KudoCode.Web.Infrastructure.Blazor.Extension;
using KudoCode.Web.Infrastructure.Domain;
using KudoCode.Web.Infrastructure.Domain.Execution;
using KudoCode.Web.Infrastructure.Domain.Handlers;
using KudoCode.Web.Infrastructure.Domain.HttpConnector;

namespace KudoCode.Mobile.Blazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

	        services.AddSingleton<IStorage, KudoCodeLocalStorage>();
	        services.AddSingleton<ITokenCache, CsrfLocalTokenCache>();

	        services.AddAutoMapper(typeof(Startup).Assembly);
	        services.AddKudoCodeNavigation();
	        services.AddScoped<IApplicationUserContext, ApplicationUserContext>();
	        services.AddScoped<WebHandlerController>();
	        services.AddScoped<IWebHandler<GetTokenRequest, int>, GetTokenRequestWebHandler>();
	        services.AddScoped<IApplicationContextHandler, ApplicationContextHandler>();
	        services.AddScoped<HttpHandler>();
	        services.AddScoped(typeof(ApiPostHandler<,>));
	        services.AddScoped<ApiPostController>();
	        services.AddScoped<WebHandlerController>();
	        services.AddEnvironmentConfiguration<Startup>(() =>
		        new EnvironmentChooser("development")
			        .Add("localhost", "development")
			        .Add("kudocode.me", "Production", false));

	        ServicesHelper.ConfigureCommonServices(services);

			services.AddMvc().AddNewtonsoftJson();
            services.AddServerSideBlazor();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            // Server Side Blazor doesn't register HttpClient by default
            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.BaseUri)
                    };
                });
            }

            ServicesHelper.ConfigureCommonServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseClientSideBlazorFiles<KudoCode.Mobile.Blazor.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToPage("/server_index");
            });

            BlazorMobileService.EnableClientToDeviceRemoteDebugging("127.0.0.1", 8888);
            BlazorMobileService.Init((bool success) =>
            {
                Console.WriteLine($"Initialization success: {success}");
                Console.WriteLine("Device is: " + BlazorDevice.RuntimePlatform);
            });
        }
    }
}
