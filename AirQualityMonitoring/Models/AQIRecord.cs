namespace AirQualityMonitoring.Models
{
    public class AQIRecord
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
        public int AQIValue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
