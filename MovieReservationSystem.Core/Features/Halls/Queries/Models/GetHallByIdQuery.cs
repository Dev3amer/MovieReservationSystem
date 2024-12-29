using MediatR;
using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Halls.Queries.Models
{
    public class GetHallByIdQuery : IRequest<Response<GetHallByIdResponse>>
    {
        public int HallId { get; set; }
    }
}
