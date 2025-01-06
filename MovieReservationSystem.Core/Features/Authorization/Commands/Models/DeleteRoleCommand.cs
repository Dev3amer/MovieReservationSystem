using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<bool>>
    {
        public string RoleId { get; set; } = default!;
    }
}
