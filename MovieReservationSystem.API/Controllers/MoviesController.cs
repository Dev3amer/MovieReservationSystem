using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Movies.Commands.Models;
using MovieReservationSystem.Core.Features.Movies.Queries.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : AppController
    {
        #region Constructors
        public MoviesController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [AllowAnonymous]
        [HttpGet(Router.MovieRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var result = await _mediator.Send(new GetAllMoviesQuery());
            return NewResult(result);
        }

        [AllowAnonymous]
        [HttpGet(Router.MovieRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetMovieByIdQuery() { Id = id });
            return NewResult(result);
        }

        [AllowAnonymous]
        [HttpGet(Router.MovieRouting.PaginatedList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMoviesPaginatedList([FromQuery] GetMoviesPaginatedListQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }
        #endregion

        #region Commands Actions
        [HttpPost(Router.MovieRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMovie([FromBody] CreateMovieCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }
        [HttpPut(Router.MovieRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditMovie([FromBody] EditMovieCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }
        [HttpDelete(Router.MovieRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _mediator.Send(new DeleteMovieCommand() { MovieId = id });
            return NewResult(result);
        }
        #endregion
    }
}
