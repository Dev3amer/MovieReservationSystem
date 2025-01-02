using MediatR;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Seats.Queries.Models
{
    public class GetFreeSeatsInShowTimeQuery : IRequest<Response<List<GetFreeSeatsInShowTimeResponse>>>
    {
        public int ShowTimeId { get; set; }
    }
}
