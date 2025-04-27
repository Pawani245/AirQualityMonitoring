using AirQualityMonitoring.Data;
using AirQualityMonitoring.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirQualityMonitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SensorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            return Ok(await _context.Sensors.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSensor(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
            return Ok(sensor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, Sensor updatedSensor)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
                return NotFound();

            sensor.SensorName = updatedSensor.SensorName;
            sensor.Latitude = updatedSensor.Latitude;
            sensor.Longitude = updatedSensor.Longitude;
            sensor.IsActive = updatedSensor.IsActive;

            await _context.SaveChangesAsync();
            return Ok(sensor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
                return NotFound();

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
