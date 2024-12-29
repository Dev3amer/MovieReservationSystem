using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile
    {
        public void CreateSeatTypeMapping()
        {
            CreateMap<CreateSeatTypeCommand, SeatType>()
                .ForMember(st => st.Seats, option => option.Ignore());
        }
    }
}
