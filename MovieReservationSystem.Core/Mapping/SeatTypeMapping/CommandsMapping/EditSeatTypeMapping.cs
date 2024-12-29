using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile
    {
        public void EditSeatTypeMapping()
        {
            CreateMap<EditSeatTypeCommand, SeatType>()
                .ForMember(st => st.Seats, option => option.Ignore());
        }
    }
}