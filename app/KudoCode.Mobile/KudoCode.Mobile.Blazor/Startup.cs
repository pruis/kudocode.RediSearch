using BlazorMobile.Common;
using BlazorMobile.Common.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
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

namespace KudoCode.Mobile.Blazor
{
    public class Startup
    {
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
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            #region DEBUG

            //Only if you want to test WebAssembly with remote debugging from a dev machine
            BlazorMobileService.EnableClientToDeviceRemoteDebugging("192.168.1.118", 8888);

            #endregion

            BlazorMobileService.Init((bool success) =>
            {
                Console.WriteLine($"Initialization success: {success}");
                Console.WriteLine("Device is: " + BlazorDevice.RuntimePlatform);
            });

            app.AddComponent<MobileApp>("app");
        }
    }
}
