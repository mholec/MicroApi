using System;
using System.Threading.Tasks;
using MicroApi.Models;
using MicroApi.Services;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Handlers
{
    public class ForecastHandler : HandlerBase
    {
        private readonly IForecastService forecastService;
        
        public ForecastHandler(IForecastService forecastService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.forecastService = forecastService;
        }

        public async Task GetForecast()
        {
            WeatherForecast weatherForecast = new WeatherForecast();
            weatherForecast.Date = DateTime.Now;
            
            await Ok(weatherForecast);
        }
        
        

        public async Task GetForecastById()
        {
            int id = GetRouteValue<int>("id");

            await Ok(forecastService.GetById(id));
        }
    }
}