using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Features.Seats.Queries.Results.Shared;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile
    {
        public void GetSeatByIdMapping()
        {
            CreateMap<Seat, GetSeatByIdResponse>();


            CreateMap<Hall, HallInSeatResponse>();

            CreateMap<SeatType, SeatTypeInSeatResponse>();
        }
    }
}
