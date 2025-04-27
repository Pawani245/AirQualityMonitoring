namespace AirQualityMonitoring.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string SensorName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
