using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AirQualityMonitoring.Models;

namespace AirQualityMonitoring.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<AQIRecord> AQIRecords { get; set; }
        public DbSet<AlertThreshold> AlertThresholds { get; set; }
    }
}
