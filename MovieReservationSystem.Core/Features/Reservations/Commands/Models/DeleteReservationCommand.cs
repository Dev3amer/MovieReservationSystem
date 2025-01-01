using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Reservations.Commands.Models
{
    public class DeleteReservationCommand : IRequest<Response<bool>>
    {
        public int ReservationId { get; set; }
    }
}
