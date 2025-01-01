using AutoMapper;
using MediatR;
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
        #endregion

        #region Constructors
        public ActorCommandHandler(IActorService actorService, IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<GetActorByIdResponse>> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {

            var actor = _mapper.Map<Actor>(request);

            var savedActor = await _actorService.AddAsync(actor);
            var response = _mapper.Map<GetActorByIdResponse>(savedActor);
            return Success(response);
        }
        public async Task<Response<GetActorByIdResponse>> Handle(EditActorCommand request, CancellationToken cancellationToken)
        {
            var oldActor = await _actorService.GetByIdAsync(request.ActorId);
            var mappedActor = _mapper.Map(request, oldActor);
            var savedActor = await _actorService.EditAsync(mappedActor);
            var response = _mapper.Map<GetActorByIdResponse>(savedActor);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _actorService.GetByIdAsync(request.ActorId);

            var isDeleted = await _actorService.DeleteAsync(actor);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
