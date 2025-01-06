using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Seeding;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class SeatTypeConfiguration : IEntityTypeConfiguration<SeatType>
    {
        public void Configure(EntityTypeBuilder<SeatType> builder)
        {
            builder.HasKey(st => st.SeatTypeId);
            builder.Property(st => st.SeatTypeId).UseIdentityColumn();

            builder.Property(st => st.TypeName)
                .HasColumnType("nvarchar")
                .HasMaxLength(55)
                .IsRequired();

            builder.Property(st => st.SeatTypePrice)
                .HasColumnType("decimal")
                .HasPrecision(5, 2)
                .IsRequired();

            builder.ToTable("SeatTypes");
            builder.HasData(SeedingDataWithMigration.LoadDefaultSeatTypes());
        }
    }
}
