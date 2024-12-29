using MovieReservationSystem.Core.Features.Seats.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile
    {
        public void CreateSeatMapping()
        {
            CreateMap<CreateSeatCommand, Seat>();
        }
    }
}
