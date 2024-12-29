using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class ShowTimeConfiguration : IEntityTypeConfiguration<ShowTime>
    {
        public void Configure(EntityTypeBuilder<ShowTime> builder)
        {
            builder.HasKey(st => st.ShowTimeId);


            builder.Property(st => st.ShowTimePrice)
                .HasColumnType("decimal")
                .HasPrecision(5, 2)
                .IsRequired();

            builder.HasOne(st => st.Movie)
                .WithMany(m => m.ShowTimes)
                .HasForeignKey(st => st.MovieId)
                .IsRequired();

            builder.HasOne(st => st.Hall)
                .WithMany(h => h.ShowTimes)
                .HasForeignKey(st => st.HallId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.ToTable("ShowTimes");
        }
    }
}
