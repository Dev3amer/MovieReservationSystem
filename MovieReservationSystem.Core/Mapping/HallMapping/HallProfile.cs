using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.HallMapping
{
    public partial class HallProfile : Profile
    {
        public HallProfile()
        {
            GetAllHallsMapping();
            GetHallByIdMapping();
            CreateHallMapping();
            EditHallMapping();
        }
    }
}
