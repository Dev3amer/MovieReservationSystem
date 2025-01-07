using MediatR;
using MovieReservationSystem.Core.Features.Authorization.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Authorization.Queries.Models
{
    public class GetUserRolesQuery : IRequest<Response<GetUserRolesResponse>>
    {
        public string userId { get; set; } = default!;
    }
}
