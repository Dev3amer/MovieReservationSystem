using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructor
        public AuthorizationService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #endregion
        #region Functions
        public async Task<IdentityRole> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole(roleName);
            var identityResult = await _roleManager.CreateAsync(role);

            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.FirstOrDefault().Description);

            role = await _roleManager.FindByNameAsync(roleName);
            return role;
        }

        public async Task<IdentityRole> EditRoleAsync(string Id, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            role.Name = roleName;

            var identityResult = await _roleManager.UpdateAsync(role);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.FirstOrDefault().Description);
            }
            return role;
        }
        public async Task<IdentityResult> DeleteRoleAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users is not null && users.Count != 0)
                throw new Exception(SharedResourcesKeys.DeleteRoleWitUsersException);
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<bool> IsRoleExistByIdAsync(string Id)
        {
            return await _roleManager.Roles.AnyAsync(r => r.Id == Id);
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles
                .Select(r => new IdentityRole { Id = r.Id, Name = r.Name })
                .ToListAsync();
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await _roleManager.Roles
                .Select(r => new IdentityRole { Id = r.Id, Name = r.Name })
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        #endregion
    }
}
