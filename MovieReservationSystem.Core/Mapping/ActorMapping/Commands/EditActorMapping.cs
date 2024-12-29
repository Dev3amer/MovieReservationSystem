using MovieReservationSystem.Core.Features.Actors.Commands.Models;
using MovieReservationSystem.Core.Features.Directors.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile
    {
        public void EditActorMapping()
        {
            CreateMap<EditActorCommand, Actor>()
            .ForPath(dist => dist.Person.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(dist => dist.Person.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForPath(dist => dist.Person.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
            .ForPath(dist => dist.Person.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForPath(dist => dist.Person.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
