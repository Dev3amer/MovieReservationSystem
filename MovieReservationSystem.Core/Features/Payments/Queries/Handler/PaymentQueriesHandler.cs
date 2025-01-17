using MediatR;
using MovieReservationSystem.Core.Features.Payments.Queries.Models;
using MovieReservationSystem.Core.Features.Payments.Queries.Results;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Payments.Queries.Handler
{
    public class PaymentQueriesHandler : ResponseHandler,
        IRequestHandler<CreateOrUpdatePaymentIntentQuery, Response<CreateOrUpdatePaymentIntentResult>>
    {
        #region Fields
        private readonly IPaymentService _paymentService;
        #endregion
        #region Constructors
        public PaymentQueriesHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        #endregion
        public async Task<Response<CreateOrUpdatePaymentIntentResult>> Handle(CreateOrUpdatePaymentIntentQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _paymentService.CreateOrUpdatePaymentIntent(request.ReservationId);
            if (reservation is null)
                return BadRequest<CreateOrUpdatePaymentIntentResult>(SharedResourcesKeys.ReservationError);

            var response = new CreateOrUpdatePaymentIntentResult()
            {
                ReservationId = reservation.ReservationId,
                ClientSecret = reservation.ClientSecret,
                HallName = reservation.ShowTime.Hall.Name,
                ShowTime = new ShowTimeInReservationResponse()
                {
                    Day = reservation.ShowTime.Day,
                    EndTime = reservation.ShowTime.EndTime,
                    ShowTimeId = reservation.ShowTime.ShowTimeId,
                    StartTime = reservation.ShowTime.StartTime,
                    MovieName = reservation.ShowTime.Movie.Title
                },
                PaymentIntentId = reservation.PaymentIntentId,
                ReservationDate = reservation.CreatedAt,
                Seats = reservation.ReservedSeats.Select(rs => new SeatsInReservationResponse
                {
                    SeatId = rs.SeatId,
                    SeatNumber = rs.SeatNumber,
                }).ToList(),
                User = new UserInReservationResponse
                {
                    Id = reservation.UserId,
                    UserName = reservation.User.UserName
                },
            };

            return Success(response);
        }
    }
}
