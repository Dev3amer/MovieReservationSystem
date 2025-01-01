using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Core.Mapping.ReservationMapping
{
    public partial class ReservationProfile
    {
        public void GetReservationByIdMapping()
        {
            CreateMap<Reservation, GetReservationByIdResponse>()
                .ForMember(dist => dist.HallName, option => option.MapFrom(src => src.ShowTime.Hall.Name));

            CreateMap<ShowTime, ShowTimeInReservationResponse>()
                .ForMember(dist => dist.MovieName, option => option.MapFrom(src => src.Movie.Title));

            CreateMap<User, UserInReservationResponse>();
            CreateMap<Seat, SeatsInReservationResponse>();

        }
    }
}
