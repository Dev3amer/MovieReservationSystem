using MovieReservationSystem.Core.Features.Movies.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.MovieMapping
{
    public partial class MovieProfile
    {
        public void AddMovieCommandMapping()
        {
            CreateMap<CreateMovieCommand, Movie>()
                .ForMember(m => m.Genres, option => option.Ignore())
                .ForMember(m => m.ShowTimes, option => option.Ignore());
        }
    }
}
