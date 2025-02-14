﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.API.APIBases;
using MovieReservationSystem.Core.Features.Actors.Commands.Models;
using MovieReservationSystem.Core.Features.Actors.Queries.Models;
using MovieReservationSystem.Core.Filters;
using MovieReservationSystem.Data.AppMetaData;

namespace MovieReservationSystem.API.Controllers
{
    [ApiController]
    public class ActorsController : AppController
    {
        #region Constructors
        public ActorsController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Queries Actions
        [Authorize(Roles = "Data Entry")]
        [HttpGet(Router.ActorRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllActorsAsync()
        {
            var result = await _mediator.Send(new GetAllActorsQuery());
            return NewResult(result);
        }

        [HttpGet(Router.ActorRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActorByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetActorByIdQuery() { ActorId = id });
            return NewResult(result);
        }
        #endregion

        #region Commands Actions
        [Authorize(Roles = "Data Entry")]
        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPost(Router.ActorRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateActor([FromForm] CreateActorCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [Authorize(Roles = "Data Entry")]
        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPut(Router.ActorRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditActor([FromForm] EditActorCommand model)
        {
            var result = await _mediator.Send(model);
            return NewResult(result);
        }

        [Authorize(Roles = "Data Entry")]
        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpDelete(Router.ActorRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var result = await _mediator.Send(new DeleteActorCommand() { ActorId = id });
            return NewResult(result);
        }
        #endregion
    }
}
