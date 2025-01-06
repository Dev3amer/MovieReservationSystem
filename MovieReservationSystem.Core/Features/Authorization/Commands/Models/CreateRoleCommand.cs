using MediatR;
using MovieReservationSystem.Core.Features.Authorization.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Authorization.Commands.Models
{
    public class CreateRoleCommand : IRequest<Response<GetRoleByIdResponse>>
    {
        public string Name { get; set; } = default!;
    }
}
