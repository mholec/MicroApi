using MicroApi.Models;

namespace MicroApi.Services
{
    public class ForecastService : IForecastService
    {
        public WeatherForecast GetById(int id)
        {
            return new WeatherForecast()
            {
                Summary = id.ToString()
            };
        }
    }
}