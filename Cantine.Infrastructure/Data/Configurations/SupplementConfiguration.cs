using Cantine.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cantine.Infrastructure.Data.Configurations
{
    public class SupplementConfiguration : IEntityTypeConfiguration<Supplement>
    {
        public void Configure(EntityTypeBuilder<Supplement> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Type).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Price).HasColumnType("decimal(18, 2)").IsRequired();

            // Many-to-many relationship with Meal
            builder.HasMany(s => s.Meals)
                   .WithMany(m => m.Supplements)
                   .UsingEntity(j => j.ToTable("MealSupplements"));
        }
    }
}
