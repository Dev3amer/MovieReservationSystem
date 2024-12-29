using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(d => d.DirectorId);

            builder.HasOne(a => a.Person)
                 .WithOne(p => p.Director)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Movies)
                .WithOne(m => m.Director)
                .HasForeignKey(m => m.DirectorId);

            builder.ToTable("Directors");
        }
    }
}
