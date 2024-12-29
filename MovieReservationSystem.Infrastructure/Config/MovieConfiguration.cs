using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.MovieId);

            builder.Property(m => m.Title)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2500)
                .IsRequired();


            builder.Property(m => m.PosterURL)
                .HasColumnType("NVARCHAR(max)")
                .IsRequired();

            builder.Property(m => m.Rate)
                .HasColumnType("decimal")
                .HasPrecision(2, 1)
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity<MovieGenre>();

            builder.ToTable("Movies");
        }
    }
}
