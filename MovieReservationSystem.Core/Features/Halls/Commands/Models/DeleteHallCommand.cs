using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Halls.Commands.Models
{
    public class DeleteHallCommand : IRequest<Response<bool>>
    {
        public int HallId { get; set; }
    }
}
