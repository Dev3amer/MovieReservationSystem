using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.ShowTimeMapping
{
    public partial class ShowTimeProfile : Profile
    {
        public ShowTimeProfile()
        {
            GetShowTimeByIdMapping();
            GetAllShowTimesMapping();
            CreateShowTimeMapping();
            EditShowTimeMapping();
        }
    }
}
