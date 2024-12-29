using MediatR;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.SeatTypes.Queries.Models
{
    public class GetAllSeatTypesQuery : IRequest<Response<List<GetAllSeatTypesResponse>>>
    {
    }
}
