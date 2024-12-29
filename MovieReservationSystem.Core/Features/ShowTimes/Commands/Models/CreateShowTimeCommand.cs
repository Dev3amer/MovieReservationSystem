using MediatR;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.ShowTimes.Commands.Models
{
    public class CreateShowTimeCommand : IRequest<Response<GetShowTimeByIdResponse>>
    {
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal ShowTimePrice { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
    }
}
