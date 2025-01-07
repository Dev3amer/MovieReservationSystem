using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Authentication.Commands.Models;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Helpers;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationCommandsHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthTokenResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthTokenResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        #endregion
        #region Constructors
        public AuthenticationCommandsHandler(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        public async Task<Response<JwtAuthTokenResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user Exist by Username
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequest<JwtAuthTokenResponse>(SharedResourcesKeys.InvalidUserName);

            //Check if Password is true for User
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
            {
                return BadRequest<JwtAuthTokenResponse>(SharedResourcesKeys.IncorrectPassword);
            }

            //Generate JWTAuthToken
            var response = await _authenticationService.GetJwtTokenAsync(user);

            //return token
            return Success(response);
        }

        public async Task<Response<JwtAuthTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //Read Access Token To Get Claims 
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var validationResult = await _authenticationService.ValidateBeforeRenewTokenAsync(jwtToken, request.AccessToken, request.RefreshToken);

            if (validationResult != null)
                return Unauthorized<JwtAuthTokenResponse>(validationResult);


            var userRefreshToken = await _authenticationService.GetUserFullRefreshTokenObjByRefreshToken(request.RefreshToken);
            var result = await _authenticationService.CreateNewAccessTokenByRefreshToken(request.AccessToken, userRefreshToken);
            return Success(result);
        }
    }
}
