using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Genres.Commands.Models;
using MovieReservationSystem.Core.Features.Genres.Queries.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GenresController : AppController
    {
        #region Constructors
        public GenresController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [HttpGet(Router.GenreRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            var result = await _mediator.Send(new GetAllGenresQuery());
            return NewResult(result);
        }

        [HttpGet(Router.GenreRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGenreByIdAsync(byte id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery() { GenreId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.GenreRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddGenre([FromBody] CreateGenreCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpPut(Router.GenreRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditGenre([FromBody] EditGenreCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpDelete(Router.GenreRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGenre(byte id)
        {
            var result = await _mediator.Send(new DeleteGenreCommand() { GenreId = id });
            return NewResult(result);
        }
        #endregion
    }
}
