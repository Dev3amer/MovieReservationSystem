using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasKey(h => h.HallId);

            builder.Property(h => h.Name)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.ToTable("Halls");
        }
    }
}
