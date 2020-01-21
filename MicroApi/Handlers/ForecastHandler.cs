using System;
using System.Threading.Tasks;
using MicroApi.Models;
using MicroApi.Services;
using Microsoft.AspNetCore.Http;
using WebApiContrib.Core.Results;

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

            await HttpContext.Ok(weatherForecast);
        }

        public async Task GetForecastById()
        {
            int id = GetRouteValue<int>("id");

            await HttpContext.Ok(forecastService.GetById(id));
        }
    }
}