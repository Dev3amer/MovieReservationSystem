using MovieReservationSystem.Core.Features.Genres.Commands.Models;
using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile
    {
        public void EditGenreMapping()
        {
            CreateMap<EditGenreCommand, Genre>()
                .ForMember(g => g.Movies, option => option.Ignore());
        }
    }
}