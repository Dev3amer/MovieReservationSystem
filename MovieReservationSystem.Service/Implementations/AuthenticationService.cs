using Microsoft.IdentityModel.Tokens;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Helpers;
using MovieReservationSystem.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieReservationSystem.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;


        #endregion
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        #region Constructors

        #endregion

        #region Actions
        public Task<string> GetJwtToken(User user)
        {
            var userClaims = new List<Claim>()
            {
                new Claim("username",user.UserName),
                new Claim("Email",user.Email),
                new Claim("PhoneNumber",user.PhoneNumber)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: signingCred
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(accessToken);
        }
        #endregion

    }
}