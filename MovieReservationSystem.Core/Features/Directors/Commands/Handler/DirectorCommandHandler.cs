using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Directors.Commands.Models;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Handler
{
    public class DirectorCommandHandler : ResponseHandler,
        IRequestHandler<CreateDirectorCommand, Response<GetDirectorByIdResponse>>,
        IRequestHandler<EditDirectorCommand, Response<GetDirectorByIdResponse>>,
        IRequestHandler<DeleteDirectorCommand, Response<bool>>
    {
        #region Fields
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DirectorCommandHandler(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<GetDirectorByIdResponse>> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = _mapper.Map<Director>(request);
            var savedDirector = await _directorService.AddAsync(director);
            var response = _mapper.Map<GetDirectorByIdResponse>(savedDirector);
            return Success(response);
        }
        public async Task<Response<GetDirectorByIdResponse>> Handle(EditDirectorCommand request, CancellationToken cancellationToken)
        {
            var oldDirector = await _directorService.GetByIdAsync(request.DirectorId);
            var mappedDirector = _mapper.Map(request, oldDirector);
            var savedDirector = await _directorService.EditAsync(mappedDirector);
            var response = _mapper.Map<GetDirectorByIdResponse>(savedDirector);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _directorService.GetByIdAsync(request.DirectorId);

            var isDeleted = await _directorService.DeleteAsync(director);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
