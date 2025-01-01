using MediatR;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Seats.Queries.Models
{
    public class GetSeatByIdQuery : IRequest<Response<GetSeatByIdResponse>>
    {
        public int SeatId { get; set; }
    }
}
