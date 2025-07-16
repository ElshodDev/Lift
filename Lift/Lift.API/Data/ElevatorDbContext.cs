using Lift.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.API.Data
{
    public class ElevatorDbContext : DbContext
    {
        public ElevatorDbContext(DbContextOptions<ElevatorDbContext> options) : base(options) { }

        public DbSet<ElevatorStatus> ElevatorStatuses { get; set; }
        public DbSet<ElevatorRequest> ElevatorRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElevatorStatus>()
                .Property(e => e.Direction)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ElevatorRequest>()
               .Property(r => r.RequestTime)
               .HasDefaultValueSql("CURRENT_TIMESTAMP"); // so‘rov vaqti default
        }
    }
}
