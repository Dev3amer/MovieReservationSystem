using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.PersonId);

            builder.Property(a => a.FirstName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(55)
                .IsRequired();

            builder.Property(a => a.LastName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(55)
                .IsRequired();

            builder.Property(a => a.ImageURL)
                .HasColumnType("NVARCHAR(max)")
                .IsRequired();

            builder.Property(m => m.Bio)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2500)
                .IsRequired();

            builder.ToTable("People");
        }
    }
}
