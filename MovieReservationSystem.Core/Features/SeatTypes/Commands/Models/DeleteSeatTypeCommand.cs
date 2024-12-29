using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Models
{
    public class DeleteSeatTypeCommand : IRequest<Response<bool>>
    {
        public byte SeatTypeId { get; set; }
    }
}
