using AirQualityMonitoring.Data;
using AirQualityMonitoring.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirQualityMonitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AQIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new Random();

        public AQIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("simulate")]
        public async Task<IActionResult> SimulateAQIData()
        {
            var sensors = await _context.Sensors.Where(s => s.IsActive).ToListAsync();

            foreach (var sensor in sensors)
            {
                var aqiValue = _random.Next(0, 300); // AQI from 0 (Good) to 300 (Hazardous)
                var record = new AQIRecord
                {
                    SensorId = sensor.Id,
                    AQIValue = aqiValue,
                    Timestamp = DateTime.UtcNow
                };
                _context.AQIRecords.Add(record);
            }

            await _context.SaveChangesAsync();
            return Ok("AQI data simulated successfully!");
        }
    }
}
