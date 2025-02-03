using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // Configure primary key
            builder.HasKey(t => t.Id);

            // Configure properties
            builder.Property(t => t.ClientId)
                   .IsRequired()
                   .HasComment("ID of the client");

            builder.Property(t => t.MealId)
                   .IsRequired()
                   .HasComment("ID of the meal");

            builder.Property(t => t.TotalAmount)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("Total amount of the ticket");

            builder.Property(t => t.EmployerCoverage)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("Employer's coverage amount");

            builder.Property(t => t.AmountToPay)
                   .HasPrecision(18, 2)
                   .IsRequired()
                   .HasDefaultValue(0)
                   .HasComment("Amount to be paid by the client");

            // Configure indexes
            builder.HasIndex(t => t.ClientId).HasDatabaseName("IX_Ticket_ClientId");
            builder.HasIndex(t => t.MealId).HasDatabaseName("IX_Ticket_MealId");

            // One-to-many relationship with Client
            builder.HasOne(t => t.Client)
                   .WithMany(c => c.Tickets)
                   .HasForeignKey(t => t.ClientId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Ticket_Client");

            // One-to-many relationship with Meal
            builder.HasOne(t => t.Meal)
                   .WithMany(m => m.Tickets)
                   .HasForeignKey(t => t.MealId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Ticket_Meal");

            // Configuration for the list of products (JSON serialization)
            builder.Property(t => t.Products)
                   .HasConversion(
                       v => string.Join(',', v), // Convert list to string
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert string to list
                   )
                   .HasComment("List of products in the ticket");

            // Add table comment
            builder.ToTable("Tickets").HasComment("Table to store ticket information");
        }
    }
}
