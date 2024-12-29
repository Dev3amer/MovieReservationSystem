using MediatR;
using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Users.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public string Id { get; set; } = default!;

    }
}
