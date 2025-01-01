using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Movies.Queries.Models;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Features.Movies.Queries.Results.Shared;
using MovieReservationSystem.Core.Pagination;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Movies.Queries.Handlers
{
    public class MoviesQueryHandler : ResponseHandler,
        IRequestHandler<GetAllMoviesQuery, Response<List<GetAllMoviesResponse>>>,
        IRequestHandler<GetMovieByIdQuery, Response<GetMovieByIdResponse>>,
        IRequestHandler<GetMoviesPaginatedListQuery, PaginatedList<GetMoviesPaginatedListResponse>>
    {
        #region Fields
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public MoviesQueryHandler(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<List<GetAllMoviesResponse>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var moviesList = await _movieService.GetAllAsync();

            var mappedMoviesList = _mapper.Map<List<GetAllMoviesResponse>>(moviesList);

            return Success(mappedMoviesList);
        }

        public async Task<Response<GetMovieByIdResponse>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieService.GetByIdAsync(request.Id);
            if (movie is null)
                return NotFound<GetMovieByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedMovie = _mapper.Map<GetMovieByIdResponse>(movie);

            return Success(mappedMovie);
        }

        public async Task<PaginatedList<GetMoviesPaginatedListResponse>> Handle(GetMoviesPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var moviesListQueryable = _movieService.GetAllQueryable(request.Search, request.MovieOrdering);

            var PaginatedList = await moviesListQueryable
                .Select(m => new GetMoviesPaginatedListResponse
                {
                    MovieId = m.MovieId,
                    DurationInMinutes = m.DurationInMinutes,
                    Description = m.Description,
                    IsActive = m.IsActive,
                    PosterURL = m.PosterURL,
                    ReleaseYear = m.ReleaseYear,
                    Rate = m.Rate,
                    Title = m.Title,
                    Genres = m.Genres.Select(mg =>
                            new GenreInMovieResponse { GenreId = mg.GenreId, Name = mg.Name }),
                    Actors = m.Actors.Select(ma =>
                            new ActorInMovieResponse { ActorId = ma.ActorId, ActorName = ma.Person.FirstName + " " + ma.Person.LastName }),
                    Director = new DirectorInMovieResponse { DirectorId = m.Director.DirectorId, DirectorName = m.Director.Person.FirstName + " " + m.Director.Person.LastName },
                    ShowTimes = m.ShowTimes.Select(ms =>
                            new ShowTimeInMovieResponse { ShowTimeId = ms.ShowTimeId, Day = ms.Day, StartTime = ms.StartTime, EndTime = ms.EndTime })
                })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return PaginatedList;
        }
        #endregion
    }
}