using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>()
                .ForMember(dist => dist.FullName, option => option.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
