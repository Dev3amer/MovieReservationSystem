using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile : Profile
    {
        public SeatTypeProfile()
        {
            GetAllSeatTypesMapping();
            GetSeatTypeByIdMapping();
            CreateSeatTypeMapping();
            EditSeatTypeMapping();
        }
    }
}