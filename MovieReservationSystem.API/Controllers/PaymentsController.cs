using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Payments.Queries.Models;
using MovieReservationSystem.Data.AppMetaData;
using MovieReservationSystem.Service.Abstracts;
using Stripe;

namespace MovieReservationSystem.API.Controllers
{
    [ApiController]
    public class PaymentsController : AppController
    {
        private readonly string _webHookSecret;
        private readonly IPaymentService _paymentService;
        #region Constructors
        public PaymentsController(IMediator mediator, IPaymentService paymentService, IConfiguration configuration) : base(mediator)
        {
            _webHookSecret = configuration["StripeSettings:webHookSecret"];
            _paymentService = paymentService;
        }
        #endregion

        [Authorize]
        [HttpPost(Router.PaymentRouting.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrUpdatePaymentIntentQuery(int reservationId)
        {
            var result = await _mediator.Send(new CreateOrUpdatePaymentIntentQuery { ReservationId = reservationId });
            return NewResult(result);
        }

        [HttpPost(Router.PaymentRouting.webhook)]
        public async Task<IActionResult> HandleWebhookAsync()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _webHookSecret
                );

                // Handle the event
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                        break;
                    case "payment_intent.failed":
                        await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}