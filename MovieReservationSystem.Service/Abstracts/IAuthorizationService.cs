﻿using Microsoft.AspNetCore.Identity;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<IdentityRole> CreateRoleAsync(string roleName);
        Task<IdentityRole> EditRoleAsync(string Id, string roleName);
        Task<bool> IsRoleExistByIdAsync(string Id);
        Task<IdentityResult> DeleteRoleAsync(string Id);
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleByIdAsync(string id);
    }
}