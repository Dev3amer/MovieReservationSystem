using MediatR;
using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Users.Queries.Models
{
    public class GetAllUsersQuery : IRequest<Response<List<GetAllUsersResponse>>>
    {
    }
}
