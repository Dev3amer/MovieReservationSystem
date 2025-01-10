using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.FirstName)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(55)
               .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(55)
                .IsRequired();

            builder.Property(u => u.Code)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2000)
                .IsRequired(false);
        }
    }
}
