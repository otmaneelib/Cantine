using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Budget).HasPrecision(18, 2);
            builder.Property(c => c.Type).IsRequired().HasMaxLength(50);

            // One-to-many relationship with Ticket
            builder.HasMany(c => c.Tickets)
                   .WithOne(t => t.Client)
                   .HasForeignKey(t => t.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
