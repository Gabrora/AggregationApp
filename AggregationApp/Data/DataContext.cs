using AggregationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<AggregatedElectricity> AggregatedElectricities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AggregatedElectricity>()
                .Property(b => b.Pplus)
                .HasPrecision(14, 6);

            modelBuilder.Entity<AggregatedElectricity>()
                .Property(b => b.Pminus)
                .HasPrecision(14, 6);
        }
    }
}
