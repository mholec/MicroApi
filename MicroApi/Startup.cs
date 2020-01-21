using Autofac;
using MicroApi.Handlers;
using MicroApi.Middlewares;
using MicroApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicroApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddAuthorization();
            services.AddControllers(); // kvůli contrib

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseRouting();
            app.UseAuthorization();
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