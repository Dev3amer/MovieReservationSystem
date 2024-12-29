using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.ShowTimes.Commands.Models
{
    public class DeleteShowTimeCommand : IRequest<Response<bool>>
    {
        public int ShowTimeId { get; set; }
    }
}
