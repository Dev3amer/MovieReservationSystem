using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Seats.Commands.Models;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Seats.Commands.Handler
{
    public class SeatCommandsHandler : ResponseHandler,
        IRequestHandler<CreateSeatCommand, Response<GetSeatByIdResponse>>,
        IRequestHandler<DeleteSeatCommand, Response<bool>>
    {
        #region Fields
        private readonly ISeatService _seatService;
        private readonly IHallService _hallService;
        private readonly ISeatTypeService _seatTypeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatCommandsHandler(ISeatService seatService, IMapper mapper, IHallService hallService, ISeatTypeService seatTypeService)
        {
            _seatService = seatService;
            _mapper = mapper;
            _hallService = hallService;
            _seatTypeService = seatTypeService;
        }
        #endregion

        public async Task<Response<GetSeatByIdResponse>> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.GetByIdAsync(request.HallId);
            var seatsCountInHall = await _seatService.CountSeatsInHall(hall.HallId);
            if (hall.Capacity <= seatsCountInHall)
                return BadRequest<GetSeatByIdResponse>(SharedResourcesKeys.ExceedSeatsNumber);

            var seatType = await _seatTypeService.GetByIdAsync(request.SeatTypeId);

            var seat = _mapper.Map<Seat>(request);
            seat.Hall = hall;
            seat.SeatType = seatType;

            var savedSeat = await _seatService.AddAsync(seat);
            var response = _mapper.Map<GetSeatByIdResponse>(savedSeat);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteSeatCommand request, CancellationToken cancellationToken)
        {
            var seat = await _seatService.GetByIdAsync(request.SeatId);

            var isDeleted = await _seatService.DeleteAsync(seat);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
