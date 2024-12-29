using MovieReservationSystem.Core.Features.Users.Commands.Models;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
