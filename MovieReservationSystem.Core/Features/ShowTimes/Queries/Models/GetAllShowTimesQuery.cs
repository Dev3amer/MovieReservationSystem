using MediatR;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.ShowTimes.Queries.Models
{
    public class GetAllShowTimesQuery : IRequest<Response<List<GetAllShowTimesResponse>>>
    {
    }
}
