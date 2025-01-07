using Microsoft.AspNetCore.Identity;

namespace MovieReservationSystem.Infrastructure.Seeding
{
    public static class RolesSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");
            var dataEntryRole = new IdentityRole("Data Entry");
            var cinemaManagerRole = new IdentityRole("Cinema Manager");
            var reservationManagerRole = new IdentityRole("Reservations Manager");

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);
                await roleManager.CreateAsync(dataEntryRole);
                await roleManager.CreateAsync(cinemaManagerRole);
                await roleManager.CreateAsync(reservationManagerRole);
            }
        }
    }
}
