using MediatR;
using MovieReservationSystem.Core.Features.Authorization.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : IRequest<Response<GetUserRolesResponse>>
    {
        public string UserId { get; set; } = default!;
        public List<string> RolesNames { get; set; } = [];
    }
}
