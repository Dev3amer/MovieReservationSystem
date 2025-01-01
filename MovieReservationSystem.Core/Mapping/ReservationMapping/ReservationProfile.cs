using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.ReservationMapping
{
    public partial class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            GetReservationByIdMapping();
        }
    }
}
