using AirQualityMonitoring.Data;
using AirQualityMonitoring.Models;
using Microsoft.EntityFrameworkCore;

namespace AirQualityMonitoring.Services
{
    public class AQIDataGeneratorService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Random _random = new Random();

        public AQIDataGeneratorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var sensors = await context.Sensors.Where(s => s.IsActive).ToListAsync();

                    foreach (var sensor in sensors)
                    {
                        var aqiValue = _random.Next(0, 300); // Random AQI between 0 and 300
                        var record = new AQIRecord
                        {
                            SensorId = sensor.Id,
                            AQIValue = aqiValue,
                            Timestamp = DateTime.UtcNow
                        };
                        context.AQIRecords.Add(record);
                    }

                    await context.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Wait 5 minutes
            }
        }
    }
}
