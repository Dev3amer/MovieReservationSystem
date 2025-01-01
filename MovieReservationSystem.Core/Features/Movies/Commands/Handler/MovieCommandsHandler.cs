using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Movies.Commands.Models;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Features.Reservations.Commands.Models;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Movies.Commands.Handler
{
    public class MovieCommandsHandler : ResponseHandler,
        IRequestHandler<CreateMovieCommand, Response<GetMovieByIdResponse>>,
        IRequestHandler<EditMovieCommand, Response<GetMovieByIdResponse>>,
        IRequestHandler<DeleteMovieCommand, Response<bool>>
    {
        #region Fields
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IDirectorService _directorService;
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public MovieCommandsHandler(IMovieService movieService, IMapper mapper, IGenreService genreService, IDirectorService directorService, IActorService actorService)
        {
            _movieService = movieService;
            _mapper = mapper;
            _genreService = genreService;
            _directorService = directorService;
            _actorService = actorService;
        }
        #endregion
        public async Task<Response<GetMovieByIdResponse>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var genres = _genreService.GetAllQueryable().Where(g => request.GenresIds.Contains(g.GenreId)).ToList();
            var actors = _actorService.GetAllQueryable().Where(a => request.ActorsIds.Contains(a.ActorId)).ToList();
            var director = await _directorService.GetByIdAsync(request.DirectorId);

            var newMovie = _mapper.Map<Movie>(request);
            newMovie.Genres = genres;
            newMovie.Director = director;
            newMovie.Actors = actors;


            var createdMovie = await _movieService.AddAsync(newMovie);
            var responseMovie = _mapper.Map<GetMovieByIdResponse>(createdMovie);
            return createdMovie is not null ? Created(responseMovie)
                : BadRequest<GetMovieByIdResponse>();
        }

        public async Task<Response<GetMovieByIdResponse>> Handle(EditMovieCommand request, CancellationToken cancellationToken)
        {
            var oldMovie = await _movieService.GetByIdAsync(request.MovieId);

            var mappedMovie = _mapper.Map(request, oldMovie);

            var genres = _genreService.GetAllQueryable().Where(g => request.GenreIds.Contains(g.GenreId)).ToList();
            var actors = _actorService.GetAllQueryable().Where(a => request.ActorsIds.Contains(a.ActorId)).ToList();
            var director = await _directorService.GetByIdAsync(request.DirectorId);

            mappedMovie.Genres = genres;
            mappedMovie.Director = director;
            mappedMovie.Actors = actors;

            var editedMovie = await _movieService.EditAsync(mappedMovie);

            var responseMovie = _mapper.Map<GetMovieByIdResponse>(editedMovie);
            return editedMovie is not null ? Success(responseMovie)
                : BadRequest<GetMovieByIdResponse>();
        }

        public async Task<Response<bool>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieService.GetByIdAsync(request.MovieId);

            var isDeleted = await _movieService.DeleteAsync(movie);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
