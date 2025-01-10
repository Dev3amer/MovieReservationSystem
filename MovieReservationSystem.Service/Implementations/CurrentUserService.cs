using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Service.Abstracts;
using System.Security.Claims;

namespace MovieReservationSystem.Service.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public CurrentUserService(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        #endregion

        #region Functions
        public string GetUserId()
        {
            var claim = _contextAccessor.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier) ??
                throw new UnauthorizedAccessException();

            return claim.Value;
        }
        public async Task<User> GetUserAsync()
        {
            return await _userManager.FindByIdAsync(GetUserId()) ?? throw new UnauthorizedAccessException();
        }

        public async Task<bool> CheckIfRuleExist(string roleName)
        {
            return await _userManager.IsInRoleAsync(await GetUserAsync(), roleName);
        }
        #endregion

    }
}
