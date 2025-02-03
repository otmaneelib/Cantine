using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Configure primary key
            builder.HasKey(c => c.Id);

            // Configure properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Client's name");

            builder.Property(c => c.Budget)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0)
                   .HasComment("Client's budget");

            var converter = new EnumToStringConverter<ClientType>();
            builder.Property(c => c.Type)
                   .HasConversion(converter)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasComment("Type of client");

            // Configure indexes
            builder.HasIndex(c => c.Name).HasDatabaseName("IX_Client_Name");

            // Configure one-to-many relationship with Ticket
            builder.HasMany(c => c.Tickets)
                   .WithOne(t => t.Client)
                   .HasForeignKey(t => t.ClientId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Client_Tickets");

            // Add table comment
            builder.ToTable("Clients").HasComment("Table to store client information");
        }
    }
}
