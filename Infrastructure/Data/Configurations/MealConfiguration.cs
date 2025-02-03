using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            // Configure primary key
            builder.HasKey(m => m.Id);

            // Configure properties
            builder.Property(m => m.Entree)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Meal's entree");

            builder.Property(m => m.Plat)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Meal's main course");

            builder.Property(m => m.Dessert)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Meal's dessert");

            builder.Property(m => m.Pain)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Meal's bread");

            // Configure indexes
            builder.HasIndex(m => m.Entree).HasDatabaseName("IX_Meal_Entree");
            builder.HasIndex(m => m.Plat).HasDatabaseName("IX_Meal_Plat");
            builder.HasIndex(m => m.Dessert).HasDatabaseName("IX_Meal_Dessert");
            builder.HasIndex(m => m.Pain).HasDatabaseName("IX_Meal_Pain");

            // Many-to-many relationship with Supplement
            builder.HasMany(m => m.Supplements)
                   .WithMany(s => s.Meals)
                   .UsingEntity<Dictionary<string, object>>(
                       "MealSupplements",
                       j => j.HasOne<Supplement>().WithMany().HasForeignKey("SupplementId").HasConstraintName("FK_MealSupplements_Supplement"),
                       j => j.HasOne<Meal>().WithMany().HasForeignKey("MealId").HasConstraintName("FK_MealSupplements_Meal"))
                   .HasComment("Many-to-many relationship between Meal and Supplement");

            // One-to-many relationship with Ticket
            builder.HasMany(m => m.Tickets)
                   .WithOne(t => t.Meal)
                   .HasForeignKey(t => t.MealId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Meal_Tickets");

            // Add table comment
            builder.ToTable("Meals").HasComment("Table to store meal information");
        }
    }
}
