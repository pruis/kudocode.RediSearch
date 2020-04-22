using System;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Web.Api.Connector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KudoCode.Web.Infrastructure.Domain.Execution;
using AutoMapper;
using KudoCode.Helpers;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.Web.Blazor.Application._AutoMapper;
using KudoCode.Web.Infrastructure.Blazor.Extension;
using KudoCode.Web.Infrastructure.Domain;
using KudoCode.Web.Infrastructure.Domain.Handlers;
using KudoCode.Web.Infrastructure.Domain.HttpConnector;

namespace KudoCode.Web.Blazor.Application
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

			services.AddKudoCodeNavigation();

			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.CreateMap<decimal, string>().ConvertUsing(s => s.ToStringInvariant());
				mc.CreateMap<string, decimal>().ConvertUsing(s => s.ToDecimalInvariant());
				mc.CreateMap<DateTime, string>().ConvertUsing(s => s.ToString("yyyy-MM-dd"));
				mc.CreateMap<string, DateTime>().ConvertUsing(a => a.ToDate());
				mc.AddProfile(new AutoMapperExtensions());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

//            services.AddAutoMapper(typeof(Startup).Assembly);
//            services.AddAutoMapper(typeof(BudgetDummy).Assembly);
			services.AddRazorPages();
			services.AddHttpContextAccessor();
			services.AddServerSideBlazor();
			services.AddSingleton<IStorage, KudoCodeRedisStorage>();
			services.AddScoped<IApplicationUserContext, ApplicationUserContext>();

			services.AddSingleton<WebExecutionPipeline>();
			services.AddScoped(typeof(WebExecutionPipelineChained<,>));

			services.AddSingleton<Connector>();

			services.AddSingleton<ITokenCache, CsrfHttpTokenCache>();
			services.AddScoped<IApplicationContextHandler, ApplicationContextHandler>();
			services.AddScoped<HttpHandler>();
			services.AddScoped(typeof(ApiPostHandler<,>));
			services.AddScoped<ApiPostController>();
			services.AddScoped<WebHandlerController>();

			services.AddScoped<IWebHandler<GetTokenRequest, int>, GetTokenRequestWebHandler>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<CsrfTokenCookieMiddleware>();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}