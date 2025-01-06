using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeatTypesController : AppController
    {
        #region Constructors
        public SeatTypesController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [HttpGet(Router.SeatTypeRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeatTypesAsync()
        {
            var result = await _mediator.Send(new GetAllSeatTypesQuery());
            return NewResult(result);
        }

        [HttpGet(Router.SeatTypeRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGenreByIdAsync(byte id)
        {
            var result = await _mediator.Send(new GetSeatTypeByIdQuery() { SeatTypeId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.SeatTypeRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSeatType([FromBody] CreateSeatTypeCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpPut(Router.SeatTypeRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditSeatType([FromBody] EditSeatTypeCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpDelete(Router.SeatTypeRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeatType(byte id)
        {
            var result = await _mediator.Send(new DeleteSeatTypeCommand() { SeatTypeId = id });
            return NewResult(result);
        }
        #endregion
    }
}
