using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Infrastructure.Seeding
{
    public static class UserSeeder
    {
        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            var defaultAdmin = new User()
            {
                UserName = "admin",
                Email = "admin@project.com",
                FirstName = "Default",
                LastName = "Admin",
                PhoneNumber = "01000000000",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };


            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(defaultAdmin, "DefaultAdmin01_##");
                await userManager.AddToRoleAsync(defaultAdmin, "Admin");
            }
        }
    }
}
