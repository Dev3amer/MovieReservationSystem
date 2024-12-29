using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile
    {
        public void GetGenreByIdMapping()
        {
            CreateMap<Genre, GetGenreByIdResponse>();
        }
    }
}
