using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Seats.Commands.Models;
using MovieReservationSystem.Core.Features.Seats.Queries.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SeatsController : AppController
    {
        #region Constructors
        public SeatsController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [HttpGet(Router.SeatRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeatsAsync()
        {
            var result = await _mediator.Send(new GetAllSeatsQuery());
            return NewResult(result);
        }

        [HttpGet(Router.SeatRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeatByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetSeatByIdQuery() { SeatId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.SeatRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSeat([FromBody] CreateSeatCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpDelete(Router.SeatRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var result = await _mediator.Send(new DeleteSeatCommand() { SeatId = id });
            return NewResult(result);
        }
        #endregion
    }
}
