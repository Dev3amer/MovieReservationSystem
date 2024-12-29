using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Users.Commands.Models;
using MovieReservationSystem.Core.Features.Users.Queries.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UsersController : AppController
    {
        #region Constructors
        public UsersController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [HttpGet(Router.UserRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return NewResult(result);
        }

        [HttpGet(Router.UserRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery() { Id = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.UserRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] CreateUserCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpPut(Router.UserRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpPut(Router.UserRouting.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [HttpDelete(Router.UserRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteActor(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = id });
            return NewResult(result);
        }
        #endregion
    }
}
