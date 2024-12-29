using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile : Profile
    {
        public SeatProfile()
        {
            GetAllSeatsMapping();
            GetSeatByIdMapping();
            CreateSeatMapping();
        }
    }
}
