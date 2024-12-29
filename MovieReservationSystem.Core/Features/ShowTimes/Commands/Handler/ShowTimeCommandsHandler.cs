using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.ShowTimes.Commands.Handler
{
    public class ShowTimeCommandsHandler : ResponseHandler,
        IRequestHandler<CreateShowTimeCommand, Response<GetShowTimeByIdResponse>>,
        IRequestHandler<EditShowTimeCommand, Response<GetShowTimeByIdResponse>>,
        IRequestHandler<DeleteShowTimeCommand, Response<bool>>
    {
        #region Fields
        private readonly IShowTimeService _showTimeService;
        private readonly IHallService _hallService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ShowTimeCommandsHandler(IShowTimeService showTimeService, IMapper mapper, IHallService hallService, IMovieService movieService)
        {
            _showTimeService = showTimeService;
            _mapper = mapper;
            _hallService = hallService;
            _movieService = movieService;
        }
        #endregion

        public async Task<Response<GetShowTimeByIdResponse>> Handle(CreateShowTimeCommand request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.GetByIdAsync(request.HallId);
            var movie = await _movieService.GetByIdAsync(request.MovieId);
            if (hall == null)
                throw new Exception("no hall exist");

            if (movie == null)
                throw new Exception("no movie exist");

            var showTime = _mapper.Map<ShowTime>(request);
            showTime.Hall = hall;
            showTime.Movie = movie;

            var savedShowTime = await _showTimeService.AddAsync(showTime);
            var response = _mapper.Map<GetShowTimeByIdResponse>(savedShowTime);
            return Success(response);
        }
        public async Task<Response<GetShowTimeByIdResponse>> Handle(EditShowTimeCommand request, CancellationToken cancellationToken)
        {
            var oldShowTime = await _showTimeService.GetByIdAsync(request.ShowTimeId);
            var mappedShowTime = _mapper.Map(request, oldShowTime);
            var savedShowTime = await _showTimeService.EditAsync(mappedShowTime);
            var response = _mapper.Map<GetShowTimeByIdResponse>(savedShowTime);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteShowTimeCommand request, CancellationToken cancellationToken)
        {
            var showTime = await _showTimeService.GetByIdAsync(request.ShowTimeId);

            var isDeleted = await _showTimeService.DeleteAsync(showTime);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
