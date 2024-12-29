using MediatR;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Seats.Queries.Models
{
    public class GetAllSeatsQuery : IRequest<Response<List<GetAllSeatsResponse>>>
    {
    }
}
