using MediatR;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.ShowTimes.Queries.Models
{
    public class GetShowTimeByIdQuery : IRequest<Response<GetShowTimeByIdResponse>>
    {
        public int ShowTimeId { get; set; }
    }
}
