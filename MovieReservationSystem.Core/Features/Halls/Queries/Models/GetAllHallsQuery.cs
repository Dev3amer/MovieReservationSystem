using MediatR;
using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Halls.Queries.Models
{
    public class GetAllHallsQuery : IRequest<Response<List<GetAllHallsResponse>>>
    {
    }
}
