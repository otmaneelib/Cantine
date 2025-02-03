using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SupplementConfiguration : IEntityTypeConfiguration<Supplement>
    {
        public void Configure(EntityTypeBuilder<Supplement> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Type).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Price).HasPrecision(18, 2).IsRequired();

            // Many-to-many relationship with Meal
            builder.HasMany(m => m.Meals)
                .WithMany(s => s.Supplements)
               .UsingEntity<Dictionary<string, object>>(
                   "MealSupplements",
                   j => j.HasOne<Meal>().WithMany().HasForeignKey("MealId"),
                   j => j.HasOne<Supplement>().WithMany().HasForeignKey("SupplementId"));
        }
    }
}
