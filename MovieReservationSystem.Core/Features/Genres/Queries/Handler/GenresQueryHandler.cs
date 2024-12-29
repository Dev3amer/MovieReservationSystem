using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Core.Features.Genres.Queries.Models;
using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Genres.Queries.Handler
{
    public class GenreQueryHandler : ResponseHandler,
        IRequestHandler<GetAllGenresQuery, Response<List<GetAllGenresResponse>>>,
        IRequestHandler<GetGenreByIdQuery, Response<GetGenreByIdResponse>>
    {
        #region Fields
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GenreQueryHandler(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllGenresResponse>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genresList = await _genreService.GetAllQueryable().ToListAsync();

            var mappedGenresList = _mapper.Map<List<GetAllGenresResponse>>(genresList);

            return Success(mappedGenresList);
        }

        public async Task<Response<GetGenreByIdResponse>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.GetByIdAsync(request.GenreId);

            if (genre is null)
                return NotFound<GetGenreByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedGenre = _mapper.Map<GetGenreByIdResponse>(genre);

            return Success(mappedGenre);
        }
    }
}
