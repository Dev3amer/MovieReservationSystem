using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Authorization.Queries.Models;
using MovieReservationSystem.Core.Features.Authorization.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Authorization.Queries.Handler
{
    public class RoleQueriesHandler : ResponseHandler,
        IRequestHandler<GetAllRolesQuery, Response<List<GetAllRolesResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public RoleQueriesHandler(RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService)
        {
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Handlers
        public async Task<Response<List<GetAllRolesResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var rolesList = await _authorizationService.GetAllRolesAsync();
            var mappedList = rolesList.Select(r => new GetAllRolesResponse
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            return Success(mappedList);
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null)
                return NotFound<GetRoleByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedRole = new GetRoleByIdResponse() { Id = role.Id, Name = role.Name };
            return Success(mappedRole);
        }
        #endregion
    }
}
