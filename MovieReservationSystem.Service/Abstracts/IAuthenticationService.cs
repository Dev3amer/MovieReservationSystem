using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthTokenResponse> GetJwtTokenAsync(User user);
        JwtSecurityToken ReadJwtToken(string accessToken);
        Task<string>? ValidateBeforeRenewTokenAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<JwtAuthTokenResponse> CreateNewAccessTokenByRefreshToken(string accessToken, UserRefreshToken userRefreshToken);
        Task<string>? ValidateAccessTokenAsync(string accessToken);
        Task<UserRefreshToken> GetUserFullRefreshTokenObjByRefreshToken(string refreshToken);
    }
}
