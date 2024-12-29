using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Features.Seats.Queries.Results.Shared;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile
    {
        public void GetAllSeatsMapping()
        {
            CreateMap<Seat, GetAllSeatsResponse>();


            CreateMap<Hall, HallInSeatResponse>();

            CreateMap<SeatType, SeatTypeInSeatResponse>();
        }
    }
}
