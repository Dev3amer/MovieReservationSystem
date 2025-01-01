using MediatR;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Models
{
    public class CreateSeatTypeCommand : IRequest<Response<GetSeatTypeByIdResponse>>
    {
        public string TypeName { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }

        public CreateSeatTypeCommand(string typeName, decimal seatTypePrice)
        {
            TypeName = typeName.Trim();
            SeatTypePrice = seatTypePrice;
        }
    }
}
