using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Directors.Commands.Models;
using MovieReservationSystem.Core.Features.Directors.Queries.Models;
using MovieReservationSystem.Core.Filters;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DirectorsController : AppController
    {
        #region Constructors
        public DirectorsController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [Authorize(Roles = "Data Entry")]
        [HttpGet(Router.DirectorRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDirectorsAsync()
        {
            var result = await _mediator.Send(new GetAllDirectorsQuery());
            return NewResult(result);
        }

        [HttpGet(Router.DirectorRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDirectorByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetDirectorByIdQuery() { DirectorId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions
        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPost(Router.DirectorRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDirector([FromBody] CreateDirectorCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPut(Router.DirectorRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditDirector([FromBody] EditDirectorCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpDelete(Router.DirectorRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var result = await _mediator.Send(new DeleteDirectorCommand() { DirectorId = id });
            return NewResult(result);
        }
        #endregion
    }
}
