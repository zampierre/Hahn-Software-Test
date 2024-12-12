using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class DataUpsertJob
    {
        private readonly IWeatherForecastRepository _repository;

        public DataUpsertJob(IWeatherForecastRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync()
        {
            // Lógica de upsert
            var weatherForecasts = await _repository.GetAllAsync();

            foreach (var forecast in weatherForecasts)
            {
                if (forecast.Id == 0)
                {
                    await _repository.AddAsync(forecast); // Inserir novo
                }
                else
                {
                    await _repository.UpdateAsync(forecast); // Atualizar existente
                }
            }
        }
    }
}
