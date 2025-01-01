using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Directors.Queries.Models;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Directors.Queries.Handler
{
    public class DirectorQueryHandler : ResponseHandler,
        IRequestHandler<GetAllDirectorsQuery, Response<List<GetAllDirectorsResponse>>>,
        IRequestHandler<GetDirectorByIdQuery, Response<GetDirectorByIdResponse>>
    {
        #region Fields
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DirectorQueryHandler(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllDirectorsResponse>>> Handle(GetAllDirectorsQuery request, CancellationToken cancellationToken)
        {
            var directorsList = await _directorService.GetAllAsync();

            var mappedDirectorsList = _mapper.Map<List<GetAllDirectorsResponse>>(directorsList);

            return Success(mappedDirectorsList);
        }

        public async Task<Response<GetDirectorByIdResponse>> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
        {
            var director = await _directorService.GetByIdAsync(request.DirectorId);

            if (director is null)
                return NotFound<GetDirectorByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedDirector = _mapper.Map<GetDirectorByIdResponse>(director);

            return Success(mappedDirector);
        }
    }
}
