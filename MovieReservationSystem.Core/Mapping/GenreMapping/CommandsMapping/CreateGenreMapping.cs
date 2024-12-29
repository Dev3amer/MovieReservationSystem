using MovieReservationSystem.Core.Features.Genres.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile
    {
        public void CreateGenreMapping()
        {
            CreateMap<CreateGenreCommand, Genre>()
                .ForMember(g => g.Movies, option => option.Ignore());
        }
    }
}
