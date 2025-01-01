using MediatR;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Reservations.Queries.Models
{
    public class GetReservationByIdQuery : IRequest<Response<GetReservationByIdResponse>>
    {
        public int ReservationId { get; set; }
    }
}
