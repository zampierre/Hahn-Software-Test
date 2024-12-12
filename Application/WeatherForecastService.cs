using Application.Interfaces;
using Domain;

namespace Application;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _repository;

    public WeatherForecastService(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
    {
        return await _repository.GetWeatherForecastsAsync();
    }
}
