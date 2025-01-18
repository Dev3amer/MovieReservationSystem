using MediatR;
using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Queries.Models
{
    public class GetUserReservationsHistoryQuery : IRequest<Response<List<GetUserReservationsHistoryResponse>>>
    {
        public string Id { get; set; } = default!;
    }
}
