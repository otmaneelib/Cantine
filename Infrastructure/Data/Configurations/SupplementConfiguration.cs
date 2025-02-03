using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations
{
    public class SupplementConfiguration : IEntityTypeConfiguration<Supplement>
    {
        public void Configure(EntityTypeBuilder<Supplement> builder)
        {
            // Configure primary key
            builder.HasKey(s => s.Id);

            // Configure properties
            var converter = new EnumToStringConverter<SupplementType>();
            builder.Property(s => s.Type)
                   .HasConversion(converter)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Type of supplement");

            builder.Property(s => s.Price)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("Price of the supplement");

            // Configure indexes
            builder.HasIndex(s => s.Type).HasDatabaseName("IX_Supplement_Type");

            // Many-to-many relationship with Meal
            builder.HasMany(s => s.Meals)
                   .WithMany(m => m.Supplements)
                   .UsingEntity<Dictionary<string, object>>(
                       "MealSupplements",
                       j => j.HasOne<Meal>().WithMany().HasForeignKey("MealId").HasConstraintName("FK_MealSupplements_Meal"),
                       j => j.HasOne<Supplement>().WithMany().HasForeignKey("SupplementId").HasConstraintName("FK_MealSupplements_Supplement"))
                   .HasComment("Many-to-many relationship between Meal and Supplement");

            // Add table comment
            builder.ToTable("Supplements").HasComment("Table to store supplement information");
        }
    }
}
