using MediatR;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Models
{
    public class EditSeatTypeCommand : IRequest<Response<GetSeatTypeByIdResponse>>
    {
        public byte SeatTypeId { get; set; }
        public string TypeName { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }

        public EditSeatTypeCommand(byte seatTypeId, string typeName, decimal seatTypePrice)
        {
            SeatTypeId = seatTypeId;
            TypeName = typeName.Trim();
            SeatTypePrice = seatTypePrice;
        }
    }
}
