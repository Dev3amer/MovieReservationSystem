using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Seeding;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.GenreId);

            builder.Property(g => g.GenreId)
                .HasColumnType("tinyint")
                .UseIdentityColumn();

            builder.Property(m => m.Name)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(55)
                .IsRequired();


            builder.ToTable("Genres");

            builder.HasData(SeedingDataWithMigration.LoadDefaultGenres());
        }
    }
}
