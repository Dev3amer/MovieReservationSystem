using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Users.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Core.Features.Users.Commands.Handler
{
    public class UserCommandsHandler : ResponseHandler,
        IRequestHandler<CreateUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
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
        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Mapping CreateUserCommand ==> User
            var user = _mapper.Map<User>(request);

            //Save New User
            var createdUser = await _userManager.CreateAsync(user, request.Password);

            //Check if the User not Added
            if (!createdUser.Succeeded)
                return BadRequest<string>(createdUser.Errors.FirstOrDefault().Description);

            //return Created Response
            return Created<string>(SharedResourcesKeys.Created);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id);

            var user = _mapper.Map(request, oldUser);

            var updatedUser = await _userManager.UpdateAsync(user);
            if (!updatedUser.Succeeded)
                return BadRequest<string>(updatedUser.Errors.FirstOrDefault().Description);

            return Success<string>(SharedResourcesKeys.Success);
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
