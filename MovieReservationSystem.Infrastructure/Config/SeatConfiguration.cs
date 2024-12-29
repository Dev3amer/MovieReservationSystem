using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.SeatId);


            builder.HasIndex(s => new { s.SeatNumber, s.HallId })
                .IsUnique();

            builder.Property(s => s.SeatNumber)
                .HasColumnType("nvarchar")
                .HasMaxLength(55)
                .IsRequired();

            builder.HasOne(s => s.SeatType)
                .WithMany(st => st.Seats)
                .HasForeignKey(s => s.SeatTypeId)
                .IsRequired();

            builder.HasOne(s => s.Hall)
                .WithMany(h => h.Seats)
                .HasForeignKey(s => s.HallId)
                .IsRequired();

            builder.ToTable("Seats");
        }
    }
}
