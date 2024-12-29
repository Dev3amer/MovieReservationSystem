using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.DirectorMapping
{
    public partial class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            GetAllDirectorsMapping();
            GetDirectorByIdMapping();
            CreateDirectorMapping();
            EditDirectorMapping();
        }
    }
}
