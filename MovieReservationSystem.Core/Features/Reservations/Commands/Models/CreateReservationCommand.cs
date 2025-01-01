using MediatR;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Reservations.Commands.Models
{
    public class CreateReservationCommand : IRequest<Response<GetReservationByIdResponse>>
    {
        public int ShowTimeId { get; set; }
        public string UserId { get; set; } = default!;
        public List<int> SeatIds { get; set; }
    }
}
