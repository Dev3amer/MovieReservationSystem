using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            GetUserByIdMapping();
            CreateUserMapping();
            EditUserMapping();
        }
    }
}
