using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApiHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastRepository _repository;

        public WeatherForecastController(IWeatherForecastRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var forecasts = await _repository.GetWeatherForecastsAsync();
            return Ok(forecasts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var forecast = await _repository.GetByIdAsync(id);
            if (forecast == null)
                return NotFound();

            return Ok(forecast);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WeatherForecast forecast)
        {
            await _repository.AddAsync(forecast);
            return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WeatherForecast forecast)
        {
            if (id != forecast.Id)
                return BadRequest();

            await _repository.UpdateAsync(forecast);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
