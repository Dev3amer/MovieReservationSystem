using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.ShowTimeMapping
{
    public partial class ShowTimeProfile
    {
        public void GetShowTimeByIdMapping()
        {
            CreateMap<ShowTime, GetShowTimeByIdResponse>()
                .ForMember(dist => dist.HallName, options => options.MapFrom(src => src.Hall.Name))
                .ForMember(dist => dist.MovieTitle, options => options.MapFrom(src => src.Movie.Title));
        }
    }
}
