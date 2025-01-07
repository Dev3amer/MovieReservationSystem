using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Users.Commands.Models;
using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Resources;

namespace MovieReservationSystem.Core.Features.Users.Commands.Handler
{
    public class UserCommandsHandler : ResponseHandler,
        IRequestHandler<CreateUserCommand, Response<GetUserByIdResponse>>,
        IRequestHandler<EditUserCommand, Response<GetUserByIdResponse>>,
        IRequestHandler<DeleteUserCommand, Response<bool>>,
        IRequestHandler<ChangePasswordCommand, Response<bool>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserCommandsHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public async Task<Response<GetUserByIdResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var identityResult = await _userManager.CreateAsync(user, request.Password);


            if (!identityResult.Succeeded)
                return BadRequest<GetUserByIdResponse>(identityResult.Errors.FirstOrDefault().Description);

            user = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(user, "User");

            var userMappedIntoResponse = new GetUserByIdResponse
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };

            return Created(userMappedIntoResponse);
        }

        public async Task<Response<GetUserByIdResponse>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id);
            var user = _mapper.Map(request, oldUser);

            var updatedUser = await _userManager.UpdateAsync(user);
            if (!updatedUser.Succeeded)
                return BadRequest<GetUserByIdResponse>(updatedUser.Errors.FirstOrDefault().Description);

            var userMappedIntoResponse = new GetUserByIdResponse
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return Success(userMappedIntoResponse);
        }

        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
                return NotFound<bool>(SharedResourcesKeys.NotFound);

            var isDeleted = await _userManager.DeleteAsync(user);
            if (!isDeleted.Succeeded)
                return BadRequest<bool>(isDeleted.Errors.FirstOrDefault().Description);

            return Deleted<bool>();
        }

        public async Task<Response<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            var isPasswordChanged = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.ConfirmPassword);

            if (!isPasswordChanged.Succeeded)
                return BadRequest<bool>(isPasswordChanged.Errors.FirstOrDefault().Description);

            return Success<bool>(true);
        }
        #endregion
    }
}
