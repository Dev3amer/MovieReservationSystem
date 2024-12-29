using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile
    {
        public void GetSeatTypeByIdMapping()
        {
            CreateMap<SeatType, GetSeatTypeByIdResponse>();
        }
    }
}
