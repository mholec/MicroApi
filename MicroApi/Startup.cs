using Autofac;
using MicroApi.Handlers;
using MicroApi.Middlewares;
using MicroApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MicroApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            services.AddTransient<Handlers.Handlers>();
            services.AddTransient<ForecastHandler>();
            services.AddTransient<UserHandler>();
            services.AddTransient<DownloadHandler>();
            services.AddTransient<ExceptionHandler>();
            services.AddTransient<DownloadService>();
            services.AddTransient<IForecastService, ForecastService>();
        }
        
        public void Configure(IApplicationBuilder app, Handlers.Handlers handlers)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("", context => handlers.Forecast.GetForecast());
                endpoints.MapGet("user", context => handlers.User.GetUser());
                endpoints.MapPost("user", context => handlers.User.CreateUser());
                endpoints.MapGet("url", context => handlers.Download.GetFromUrl());
                endpoints.MapGet("{id:int}", context => handlers.Forecast.GetForecastById());
            });
        }
    }
}