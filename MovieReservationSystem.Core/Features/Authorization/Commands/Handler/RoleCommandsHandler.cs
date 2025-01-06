using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Authorization.Commands.Models;
using MovieReservationSystem.Core.Features.Authorization.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Authorization.Commands.Handler
{
    public class RoleCommandsHandler : ResponseHandler,
        IRequestHandler<CreateRoleCommand, Response<GetRoleByIdResponse>>,
        IRequestHandler<EditRoleCommand, Response<GetRoleByIdResponse>>,
        IRequestHandler<DeleteRoleCommand, Response<bool>>
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationService _authorizationService;

        #endregion

        #region Constructors
        public RoleCommandsHandler(RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService)
        {
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Actions
        public async Task<Response<GetRoleByIdResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedRole = await _authorizationService.CreateRoleAsync(request.Name);
                var mappedRole = new GetRoleByIdResponse { Id = updatedRole.Id, Name = updatedRole.Name };
                return Created(mappedRole);
            }
            catch (Exception ex)
            {
                return BadRequest<GetRoleByIdResponse>(ex.Message);
            }
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedRole = await _authorizationService.EditRoleAsync(request.Id, request.Name);
                var mappedRole = new GetRoleByIdResponse { Id = updatedRole.Id, Name = updatedRole.Name };
                return Success(mappedRole);
            }
            catch (Exception ex)
            {
                return BadRequest<GetRoleByIdResponse>(ex.Message);
            }
        }

        public async Task<Response<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identityResult = await _authorizationService.DeleteRoleAsync(request.RoleId);
                if (!identityResult.Succeeded)
                    return BadRequest<bool>(identityResult.Errors.FirstOrDefault().Description);
                return Deleted<bool>();
            }
            catch (Exception ex)
            {
                return BadRequest<bool>(ex.Message);
            }
        }
        #endregion
    }
}
