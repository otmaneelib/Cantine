using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Entree).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Plat).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Dessert).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Pain).IsRequired().HasMaxLength(100);

            // Many-to-many relationship with Supplement           
            builder.HasMany(m => m.Supplements)
                .WithMany(s => s.Meals)
               .UsingEntity<Dictionary<string, object>>(
                   "MealSupplements",
                   j => j.HasOne<Supplement>().WithMany().HasForeignKey("SupplementId"),
                   j => j.HasOne<Meal>().WithMany().HasForeignKey("MealId"));

            // One-to-many relationship with Ticket
            builder.HasMany(m => m.Tickets)
                   .WithOne(t => t.Meal)
                   .HasForeignKey(t => t.MealId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
