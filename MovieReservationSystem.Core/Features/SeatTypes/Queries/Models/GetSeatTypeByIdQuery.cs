using MediatR;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.SeatTypes.Queries.Models
{
    public class GetSeatTypeByIdQuery : IRequest<Response<GetSeatTypeByIdResponse>>
    {
        public byte SeatTypeId { get; set; }
    }
}
