using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Features.Movies.Queries.Results.Shared;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.MovieMapping
{
    public partial class MovieProfile
    {
        public void GetMovieByIdQueryMapping()
        {
            CreateMap<Movie, GetMovieByIdResponse>();

            CreateMap<Genre, GenreInMovieResponse>();

            CreateMap<Actor, ActorInMovieResponse>()
                .ForMember(dist => dist.ActorName, options => options.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName));


            CreateMap<Director, DirectorInMovieResponse>()
                .ForMember(dist => dist.DirectorName, options => options.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName));

            CreateMap<ShowTime, ShowTimeInMovieResponse>();
        }
    }
}
