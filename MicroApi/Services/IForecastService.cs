using MicroApi.Models;

namespace MicroApi.Services
{
    public interface IForecastService
    {
        WeatherForecast GetById(int id);
    }
}