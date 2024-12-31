using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Authentication.Commands.Models;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppController
    {
        #region Constructors
        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Commands Actions

        [HttpPost(Router.AuthenticationRouting.SignIn)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }
        #endregion
    }
}
