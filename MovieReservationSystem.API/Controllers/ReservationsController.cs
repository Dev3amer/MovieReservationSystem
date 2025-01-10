using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Reservations.Commands.Models;
using MovieReservationSystem.Core.Features.Reservations.Queries.Models;
using MovieReservationSystem.Core.Filters;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]

    public class ReservationsController : AppController
    {
        #region Constructors
        public ReservationsController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [ServiceFilter(typeof(ReservationManagerRoleFilter))]
        [HttpGet(Router.ReservationRouting.PaginatedList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationsPaginatedList([FromQuery] GetReservationsPaginatedListQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }
        [Authorize(Roles = "Reservations Manager,User")]
        [HttpGet(Router.ReservationRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetReservationByIdQuery() { ReservationId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions
        [Authorize(Roles = "Reservations Manager,User")]
        [HttpPost(Router.ReservationRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [ServiceFilter(typeof(ReservationManagerRoleFilter))]
        [HttpDelete(Router.ReservationRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var result = await _mediator.Send(new DeleteReservationCommand() { ReservationId = id });
            return NewResult(result);
        }
        #endregion
    }
}
