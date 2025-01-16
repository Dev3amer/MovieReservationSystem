using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Actors.Commands.Models;
using MovieReservationSystem.Core.Features.Actors.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Handler
{
    public class ActorCommandHandler : ResponseHandler,
        IRequestHandler<CreateActorCommand, Response<GetActorByIdResponse>>,
        IRequestHandler<EditActorCommand, Response<GetActorByIdResponse>>,
        IRequestHandler<DeleteActorCommand, Response<bool>>
    {
        #region Fields
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region Constructors
        public ActorCommandHandler(IActorService actorService, IMapper mapper, IFileService fileService, IHttpContextAccessor contextAccessor)
        {
            _actorService = actorService;
            _mapper = mapper;
            _fileService = fileService;
            _contextAccessor = contextAccessor;
        }
        #endregion
        public async Task<Response<GetActorByIdResponse>> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            request.FirstName = request.FirstName.Trim();
            request.LastName = request.LastName.Trim();

            var actor = _mapper.Map<Actor>(request);

            var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
            try
            {
                if (request.Image is null)
                    actor.Person.ImageURL = baseURL + _fileService.GetDefaultImagePath();
                else
                    actor.Person.ImageURL = baseURL + await _fileService.SaveImageAsync(request.Image, "actors");
            }
            catch (Exception ex)
            {
                return BadRequest<GetActorByIdResponse>(ex.Message);
            }

            var savedActor = await _actorService.AddAsync(actor);
            var response = _mapper.Map<GetActorByIdResponse>(savedActor);
            return Success(response);
        }
        public async Task<Response<GetActorByIdResponse>> Handle(EditActorCommand request, CancellationToken cancellationToken)
        {
            request.FirstName = request.FirstName.Trim();
            request.LastName = request.LastName.Trim();

            var oldActor = await _actorService.GetByIdAsync(request.ActorId);
            var oldImage = oldActor.Person.ImageURL;

            var mappedActor = _mapper.Map(request, oldActor);

            var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
            var oldImagePath = oldImage.Remove(0, baseURL.Length);

            try
            {
                if (request.Image is not null)
                {
                    mappedActor.Person.ImageURL = baseURL + await _fileService.ReplaceImageAsync(oldImagePath, request.Image, "actors");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<GetActorByIdResponse>(ex.Message);
            }

            var savedActor = await _actorService.EditAsync(mappedActor);
            var response = _mapper.Map<GetActorByIdResponse>(savedActor);
            return Success(response);
        }
        public async Task<Response<bool>> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _actorService.GetByIdAsync(request.ActorId);

            var isDeleted = await _actorService.DeleteAsync(actor);
            if (isDeleted)
            {
                var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
                _fileService.DeleteImage(actor.Person.ImageURL.Remove(0, baseURL.Length));
                return Deleted<bool>();
            }
            return BadRequest<bool>();
        }
    }
}
