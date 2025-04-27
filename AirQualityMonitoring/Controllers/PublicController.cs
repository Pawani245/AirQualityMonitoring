using AirQualityMonitoring.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirQualityMonitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublicController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("sensors")]
        public async Task<IActionResult> GetSensors()
        {
            var sensors = await _context.Sensors.Where(s => s.IsActive).ToListAsync();
            return Ok(sensors);
        }

        [HttpGet("aqi/{sensorId}")]
        public async Task<IActionResult> GetLatestAQI(int sensorId)
        {
            var latestAQI = await _context.AQIRecords
                .Where(a => a.SensorId == sensorId)
                .OrderByDescending(a => a.Timestamp)
                .FirstOrDefaultAsync();

            if (latestAQI == null)
                return NotFound();

            return Ok(latestAQI);
        }

        [HttpGet("aqi/history/{sensorId}")]
        public async Task<IActionResult> GetAQIHistory(int sensorId)
        {
            var history = await _context.AQIRecords
                .Where(a => a.SensorId == sensorId)
                .OrderByDescending(a => a.Timestamp)
                .Take(48) // Last 24 hours assuming simulation every 30 mins or 5 mins (adjust as needed)
                .ToListAsync();

            return Ok(history);
        }
    }
}
