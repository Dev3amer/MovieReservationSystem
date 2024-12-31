using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Authentication.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationCommandsHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<string>>
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
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user Exist by Username
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequest<string>(SharedResourcesKeys.InvalidUserName);

            //Check if Password is true for User
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
            {
                return BadRequest<string>(SharedResourcesKeys.IncorrectPassword);
            }

            //Generate JWT
            var token = await _authenticationService.GetJwtToken(user);

            //return token
            return Success(token);
        }
    }
}
