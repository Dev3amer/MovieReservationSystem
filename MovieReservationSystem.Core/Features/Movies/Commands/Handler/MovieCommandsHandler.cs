using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Movies.Commands.Models;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
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
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region Constructors
        public MovieCommandsHandler(IMovieService movieService, IMapper mapper, IGenreService genreService, IDirectorService directorService, IActorService actorService, IFileService fileService, IHttpContextAccessor httpContext)
        {
            _movieService = movieService;
            _mapper = mapper;
            _genreService = genreService;
            _directorService = directorService;
            _actorService = actorService;
            _fileService = fileService;
            _contextAccessor = httpContext;
        }
        #endregion
        public async Task<Response<GetMovieByIdResponse>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var genres = _genreService.GetAllQueryable().Where(g => request.GenresIds.Contains(g.GenreId)).ToList();
            var actors = _actorService.GetAllQueryable().Where(a => request.ActorsIds.Contains(a.ActorId)).ToList();
            var director = await _directorService.GetByIdAsync(request.DirectorId);

            request.Title = request.Title.Trim();

            var newMovie = _mapper.Map<Movie>(request);
            newMovie.Genres = genres;
            newMovie.Director = director;
            newMovie.Actors = actors;

            var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
            try
            {
                newMovie.PosterURL = baseURL + await _fileService.SaveImageAsync(request.Poster, "movies");
            }
            catch (Exception ex)
            {
                return BadRequest<GetMovieByIdResponse>(ex.Message);
            }


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

            request.Title = request.Title.Trim();

            mappedMovie.Genres = genres;
            mappedMovie.Director = director;
            mappedMovie.Actors = actors;

            var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
            var oldImagePath = oldMovie.PosterURL.Remove(0, baseURL.Length);

            try
            {
                if (request.Poster is not null)
                {
                    mappedMovie.PosterURL = baseURL + await _fileService.ReplaceImageAsync(oldImagePath, request.Poster, "movies");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<GetMovieByIdResponse>(ex.Message);
            }

            var editedMovie = await _movieService.EditAsync(mappedMovie);

            var responseMovie = _mapper.Map<GetMovieByIdResponse>(editedMovie);
            return editedMovie is not null ? Success(responseMovie)
                : BadRequest<GetMovieByIdResponse>();
        }

        public async Task<Response<bool>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieService.GetByIdAsync(request.MovieId);

            var isDeleted = await _movieService.DeleteAsync(movie);
            if (isDeleted)
            {
                var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
                _fileService.DeleteImage(movie.PosterURL.Remove(0, baseURL.Length));
                return Deleted<bool>();
            }
            return BadRequest<bool>();
        }
    }
}
