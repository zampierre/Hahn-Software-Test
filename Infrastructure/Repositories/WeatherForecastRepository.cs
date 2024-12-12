using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly ApplicationDbContext _context;

        public WeatherForecastRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }

        public async Task<WeatherForecast?> GetByIdAsync(int id)
        {
            return await _context.WeatherForecasts.FindAsync(id);
        }

        public async Task AddAsync(WeatherForecast weatherForecast)
        {
            await _context.WeatherForecasts.AddAsync(weatherForecast);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Update(weatherForecast);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.WeatherForecasts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Implemente o método GetWeatherForecastsAsync
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }
    }
}
