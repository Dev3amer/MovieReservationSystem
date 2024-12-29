using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Config
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(a => a.ActorId);


            builder.HasOne(a => a.Person)
                .WithOne(p => p.Actor)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Movies)
                .WithMany(m => m.Actors)
                .UsingEntity<MovieActor>();

            builder.ToTable("Actors");
        }
    }
}
