using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.ReservationId);



            // Reservation has one User
            builder.HasOne(r => r.User)
                .WithMany() // A user can have many reservations
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete


            builder.HasOne(r => r.ShowTime)
            .WithMany(st => st.Reservations)
            .HasForeignKey(r => r.ShowTimeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);


            // Many-to-many relationship between Reservation and Seat through ReservationSeat
            builder.HasMany(r => r.Seats)
                .WithMany(s => s.Reservations)
                .UsingEntity<ReservationSeat>();

            builder.ToTable("Reservations");
        }
    }
}
