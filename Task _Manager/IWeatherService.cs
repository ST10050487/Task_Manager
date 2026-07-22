using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task__Manager
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetCurrentWeatherAsync(string city);
        Task<ForecastResponse> GetForecastAsync(string city);
    }
}
