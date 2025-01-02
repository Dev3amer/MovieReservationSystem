using Microsoft.AspNetCore.Identity;

namespace MovieReservationSystem.Data.Entities.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();
        public User()
        {

        }
        public User(string firstName, string lastName)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }
    }
}
