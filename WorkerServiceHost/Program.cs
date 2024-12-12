using Application.Jobs;
using Domain;
using Hangfire;
using Infrastructure.Repositories;
using WorkerServiceHost;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddHangfire(x => x.UseSqlServerStorage("DefaultConnection"));
    services.AddHangfireServer();
    services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
    services.AddScoped<DataUpsertJob>();
    services.AddHostedService<Worker>();
});

var host = builder.Build();

host.Run();
