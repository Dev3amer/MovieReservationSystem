using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Reservations.Commands.Models;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Reservations.Commands.Handler
{
    public class ReservationCommandsHandler : ResponseHandler,
        IRequestHandler<CreateReservationCommand, Response<GetReservationByIdResponse>>,
        IRequestHandler<DeleteReservationCommand, Response<bool>>
    {
        #region Fields
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;
        private readonly IShowTimeService _showTimeService;
        private readonly ISeatService _seatService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ReservationCommandsHandler(IReservationService reservationService, UserManager<User> userManager, IShowTimeService showTimeService, ISeatService seatService, IMapper mapper)
        {
            _reservationService = reservationService;
            _userManager = userManager;
            _showTimeService = showTimeService;
            _seatService = seatService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<GetReservationByIdResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var showTime = await _showTimeService.GetByIdAsync(request.ShowTimeId);

            var seatsList = _seatService.GetAllQueryable()
                .Where(u => request.SeatIds.Contains(u.SeatId)).ToList();

            var newReservation = new Reservation()
            {
                ReservationDate = DateTime.Now,
                FinalPrice = _reservationService.CalculateReservationPrice(seatsList, showTime.ShowTimePrice),
                Seats = seatsList,
                ShowTime = showTime,
                User = user
            };

            var createdReservation = await _reservationService.AddAsync(newReservation);
            var responseReservation = _mapper.Map<GetReservationByIdResponse>(createdReservation);
            return createdReservation is not null ? Created(responseReservation)
                : BadRequest<GetReservationByIdResponse>();
        }
        public async Task<Response<bool>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationService.GetByIdAsync(request.ReservationId);
            if (reservation is null)
                return BadRequest<bool>(SharedResourcesKeys.Invalid);

            if (reservation.ShowTime.Day.Day <= DateTime.Now.Day) //12-12-2024  13-12-2024,12-12-2024  11-12-2024
                return BadRequest<bool>(SharedResourcesKeys.BadCancelRequest);

            var isDeleted = await _reservationService.DeleteAsync(reservation);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
