using MediatR;
using MovieReservationSystem.Core.Features.Payments.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Payments.Queries.Models
{
    public class CreateOrUpdatePaymentIntentQuery : IRequest<Response<CreateOrUpdatePaymentIntentResult>>
    {
        public int ReservationId { get; set; }
    }
}
