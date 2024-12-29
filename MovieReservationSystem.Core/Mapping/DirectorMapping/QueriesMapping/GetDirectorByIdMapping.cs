using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.DirectorMapping
{
    public partial class DirectorProfile
    {
        public void GetDirectorByIdMapping()
        {
            CreateMap<Director, GetDirectorByIdResponse>()
                .ForMember(dist => dist.FullName, opt => opt.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName))
                .ForMember(dist => dist.ImageURL, opt => opt.MapFrom(src => src.Person.ImageURL))
                .ForMember(dist => dist.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(dist => dist.Bio, opt => opt.MapFrom(src => src.Person.Bio));
        }
    }
}
