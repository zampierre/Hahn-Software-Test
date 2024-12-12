using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IWeatherForecastRepository
    {
        Task<IEnumerable<WeatherForecast>> GetAllAsync();
        Task<WeatherForecast?> GetByIdAsync(int id);
        Task AddAsync(WeatherForecast weatherForecast);
        Task UpdateAsync(WeatherForecast weatherForecast);
        Task DeleteAsync(int id);
        Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync();
    }
}
