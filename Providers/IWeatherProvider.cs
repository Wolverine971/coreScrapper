using System.Collections.Generic;
using coreScrape.Models;

namespace coreScrape.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
