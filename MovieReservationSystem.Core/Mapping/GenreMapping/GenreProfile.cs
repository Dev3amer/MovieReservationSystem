using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile : Profile
    {
        public GenreProfile()
        {
            GetAllGenresMapping();
            GetGenreByIdMapping();
            CreateGenreMapping();
            EditGenreMapping();
        }
    }
}