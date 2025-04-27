namespace AirQualityMonitoring.Models
{
    public class AlertThreshold
    {
        public int Id { get; set; }
        public int AQILevel { get; set; }  // e.g., 100, 150
        public string Category { get; set; }  // e.g., Moderate, Unhealthy
        public string ColorCode { get; set; } // e.g., "#FFFF00" for Yellow
    }
}
