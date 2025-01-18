using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.UserRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return NewResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Router.UserRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery() { Id = id });
            return NewResult(result);
        }

        [Authorize(Roles = "User,Reservation Manager")]
        [HttpGet(Router.UserRouting.UserReservations)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserReservationsAsync(string id)
        {
            var result = await _mediator.Send(new GetUserReservationsHistoryQuery() { Id = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.UserRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [Authorize]
        [HttpPut(Router.UserRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [Authorize]
        [HttpPut(Router.UserRouting.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(Router.UserRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = id });
            return NewResult(result);
        }

        [HttpGet(Router.UserRouting.ConfirmEmail)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var result = await _mediator.Send(new ConfirmEmailQuery() { UserId = userId, Code = code });
            return NewResult(result);
        }

        [HttpPost(Router.UserRouting.RequestPasswordReset)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestPasswordReset([FromForm] RequestPasswordResetCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Router.UserRouting.ValidatePasswordResetCode)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateResetCode([FromForm] ValidatePasswordResetCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Router.UserRouting.ResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        #endregion
    }
}
