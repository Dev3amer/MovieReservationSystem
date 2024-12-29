using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(t => t.ReservationId);


            builder.HasOne(t => t.Seat)
            .WithOne(s => s.Reservation)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.ShowTime)
            .WithMany(st => st.Reservations)
            .HasForeignKey(t => t.ShowTimeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Reservations");
        }
    }
}
