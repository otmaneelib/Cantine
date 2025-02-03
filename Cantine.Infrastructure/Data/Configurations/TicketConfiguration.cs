using Cantine.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cantine.Infrastructure.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ClientId).IsRequired();
            builder.Property(t => t.MealId).IsRequired();
            builder.Property(t => t.TotalAmount).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(t => t.EmployerCoverage).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(t => t.AmountToPay).HasColumnType("decimal(18, 2)").IsRequired();

            // One-to-many relationship with Client
            builder.HasOne(t => t.Client)
                   .WithMany(c => c.Tickets)
                   .HasForeignKey(t => t.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationship with Meal
            builder.HasOne(t => t.Meal)
                   .WithMany(m => m.Tickets)
                   .HasForeignKey(t => t.MealId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Configuration pour la liste des produits (sérialisation JSON)
            builder.Property(t => t.Products)
                   .HasConversion(
                       v => string.Join(',', v), // Convertir la liste en chaîne de caractères
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convertir la chaîne en liste
                   );
        }
    }
}
