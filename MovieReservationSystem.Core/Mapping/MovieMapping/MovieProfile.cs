using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.MovieMapping
{
    public partial class MovieProfile : Profile
    {
        public MovieProfile()
        {
            GetAllMovieMapping();
            GetMovieByIdQueryMapping();
            AddMovieCommandMapping();
            EditMovieCommandMapping();
        }
    }
}