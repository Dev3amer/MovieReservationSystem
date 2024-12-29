using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Genres.Commands.Models;
using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Handler
{
    public class GenreCommandsHandler : ResponseHandler,
        IRequestHandler<CreateGenreCommand, Response<GetGenreByIdResponse>>,
        IRequestHandler<EditGenreCommand, Response<GetGenreByIdResponse>>,
        IRequestHandler<DeleteGenreCommand, Response<bool>>
    {
        #region Fields
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GenreCommandsHandler(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<GetGenreByIdResponse>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _mapper.Map<Genre>(request);
            var savedGenre = await _genreService.AddAsync(genre);
            var response = _mapper.Map<GetGenreByIdResponse>(savedGenre);
            return Success<GetGenreByIdResponse>(response);
        }
        public async Task<Response<GetGenreByIdResponse>> Handle(EditGenreCommand request, CancellationToken cancellationToken)
        {
            var oldGenre = await _genreService.GetByIdAsync(request.GenreId);
            var mappedGenre = _mapper.Map(request, oldGenre);
            var savedGenre = await _genreService.EditAsync(mappedGenre);
            var response = _mapper.Map<GetGenreByIdResponse>(savedGenre);
            return Success<GetGenreByIdResponse>(response);
        }

        public async Task<Response<bool>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.GetByIdAsync(request.GenreId);

            var isDeleted = await _genreService.DeleteAsync(genre);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
