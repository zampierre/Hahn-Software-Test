using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Application;
using Infrastructure.Repositories;
using Domain;
using Application.Jobs;

namespace WorkerServiceHost
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public Worker(ILogger<Worker> logger, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            _backgroundJobClient = backgroundJobClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Configuração do Hangfire
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");  // Substitua pela string de conexão do seu banco de dados

            // Adiciona o servidor do Hangfire
            using (var server = new BackgroundJobServer())
            {
                // Agenda o job para rodar a cada 1 hora
                RecurringJob.AddOrUpdate<DataUpsertJob>(
                    job => job.ExecuteAsync(), "*/30 * * * * *");

                // Exibe mensagem no log quando o worker estiver ativo
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }
    }
}
