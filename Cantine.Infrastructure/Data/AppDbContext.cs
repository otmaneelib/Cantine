using Cantine.Core.Entities;
using Cantine.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cantine.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new MealConfiguration());
            modelBuilder.ApplyConfiguration(new SupplementConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
