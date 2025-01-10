using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Models;
using MovieReservationSystem.Core.Filters;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ShowTimesController : AppController
    {
        #region Constructors
        public ShowTimesController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [HttpGet(Router.ShowTimeRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShowTimesAsync()
        {
            var result = await _mediator.Send(new GetAllShowTimesQuery());
            return NewResult(result);
        }

        [HttpGet(Router.ShowTimeRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShowTimeByIdAsync(byte id)
        {
            var result = await _mediator.Send(new GetShowTimeByIdQuery() { ShowTimeId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions
        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPost(Router.ShowTimeRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShowTime([FromBody] CreateShowTimeCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPut(Router.ShowTimeRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditShowTime([FromBody] EditShowTimeCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpDelete(Router.ShowTimeRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShowTime(int id)
        {
            var result = await _mediator.Send(new DeleteShowTimeCommand() { ShowTimeId = id });
            return NewResult(result);
        }
        #endregion
    }
}
