using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Halls.Commands.Models;
using MovieReservationSystem.Core.Features.Halls.Queries.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class HallsController : AppController
    {
        #region Constructors
        public HallsController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [HttpGet(Router.HallRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllHallsAsync()
        {
            var result = await _mediator.Send(new GetAllHallsQuery());
            return NewResult(result);
        }

        [HttpGet(Router.HallRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHallByIdAsync(byte id)
        {
            var result = await _mediator.Send(new GetHallByIdQuery() { HallId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.HallRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddHall([FromBody] CreateHallCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpPut(Router.HallRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditHall([FromBody] EditHallCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpDelete(Router.HallRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var result = await _mediator.Send(new DeleteHallCommand() { HallId = id });
            return NewResult(result);
        }
        #endregion
    }
}
