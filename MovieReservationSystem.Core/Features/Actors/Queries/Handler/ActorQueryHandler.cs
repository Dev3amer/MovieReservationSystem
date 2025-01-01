using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Actors.Queries.Models;
using MovieReservationSystem.Core.Features.Actors.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Actors.Queries.Handler
{
    public class ActorQueryHandler : ResponseHandler,
        IRequestHandler<GetAllActorsQuery, Response<List<GetAllActorsResponse>>>,
        IRequestHandler<GetActorByIdQuery, Response<GetActorByIdResponse>>
    {
        #region Fields
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ActorQueryHandler(IActorService actorService, IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllActorsResponse>>> Handle(GetAllActorsQuery request, CancellationToken cancellationToken)
        {
            var actorsList = await _actorService.GetAllAsync();

            var mappedActorsList = _mapper.Map<List<GetAllActorsResponse>>(actorsList);

            return Success(mappedActorsList);
        }

        public async Task<Response<GetActorByIdResponse>> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
        {
            var actor = await _actorService.GetByIdAsync(request.ActorId);

            if (actor is null)
                return NotFound<GetActorByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedActor = _mapper.Map<GetActorByIdResponse>(actor);

            return Success(mappedActor);
        }
    }
}
