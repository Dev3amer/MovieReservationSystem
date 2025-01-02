using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Helpers;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieReservationSystem.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public async Task<JwtAuthTokenResponse> GetJwtTokenAsync(User user)
        {
            var jwtToken = GenerateJwtSecurityToken(user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var savedUserRefreshToken = await SaveUserRefreshTokenAsync(user, accessToken, jwtToken.Id);

            var refreshTokenForResponse = GetRefreshTokenForResponse(savedUserRefreshToken.ExpiryDate, user.UserName, savedUserRefreshToken.RefreshToken);

            return new JwtAuthTokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenForResponse
            };
        }
        private JwtSecurityToken GenerateJwtSecurityToken(User user)
        {
            var userClaims = GetClaims(user);

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.accessTokenExpireDateInMinutes),
                signingCredentials: signingCred
            );
            return jwtToken;
        }
        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id",user.Id),
                new Claim("username",user.UserName),
                new Claim("Email",user.Email),
                new Claim("PhoneNumber",user.PhoneNumber)
            };
            return claims;
        }
        private async Task<UserRefreshToken> SaveUserRefreshTokenAsync(User user, string accessToken, string jwtTokenId)
        {
            var userRefreshToken = new UserRefreshToken()
            {
                User = user,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.refreshTokenExpireDateInDays),
                IsRevoked = false,
                IsUsed = true,
                RefreshToken = GenerateRefreshTokenString(),
                Token = accessToken,
                JwtId = jwtTokenId
            };
            var savedUserRefreshToken = await _refreshTokenRepository.AddAsync(userRefreshToken);

            return savedUserRefreshToken;
        }
        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private RefreshTokenInJwtAuthTokeResponse GetRefreshTokenForResponse(DateTime ExpiryDate, string userName, string tokenString)
        {
            var refreshToken = new RefreshTokenInJwtAuthTokeResponse()
            {
                ExpireAt = ExpiryDate,
                UserName = userName,
                TokenString = tokenString
            };
            return refreshToken;
        }

        public async Task<JwtAuthTokenResponse> CreateNewAccessTokenByRefreshToken(string accessToken, UserRefreshToken userRefreshToken)
        {

            // Generate JWT Security Token
            var user = await _userManager.FindByIdAsync(userRefreshToken.userID);

            var generatedJwtSecurityToken = GenerateJwtSecurityToken(user);
            var NewAccessToken = new JwtSecurityTokenHandler().WriteToken(generatedJwtSecurityToken);

            var refreshTokenForResponse = GetRefreshTokenForResponse(userRefreshToken.ExpiryDate, user.UserName, userRefreshToken.RefreshToken);
            return new JwtAuthTokenResponse()
            {
                AccessToken = NewAccessToken,
                RefreshToken = refreshTokenForResponse
            };
        }
        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var handler = new JwtSecurityTokenHandler();

            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string>? ValidateBeforeRenewTokenAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            //Validations AccessToken
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                return SharedResourcesKeys.InvalidHashAlgorithm;
            if (jwtToken.ValidTo > DateTime.UtcNow)
                return SharedResourcesKeys.NotExpiredToken;

            //Get User RefreshToken
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;

            var userRefreshToken = await _refreshTokenRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.Token == accessToken && x.RefreshToken == refreshToken && x.userID == userId);

            //Validations User Refresh Token
            if (userRefreshToken == null)
                return SharedResourcesKeys.InvalidRefreshToken;

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return SharedResourcesKeys.ExpiredRefreshToken;
            }
            return null;
        }
        public async Task<string>? ValidateAccessTokenAsync(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                    return SharedResourcesKeys.InvalidToken;
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<UserRefreshToken> GetUserRefreshTokenByRefreshToken(string refreshToken)
        {

            return await _refreshTokenRepository.GetTableNoTracking()
                .Where(r => r.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();
        }
        #endregion

    }
}