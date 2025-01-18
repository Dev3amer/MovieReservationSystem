using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Core.Features.Seats.Queries.Models;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Seats.Queries.Handler
{
    public class SeatQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSeatsQuery, Response<List<GetAllSeatsResponse>>>,
        IRequestHandler<GetFreeSeatsInShowTimeQuery, Response<List<GetFreeSeatsInShowTimeResponse>>>,
        IRequestHandler<GetSeatByIdQuery, Response<GetSeatByIdResponse>>
    {
        #region Fields
        private readonly ISeatService _seatService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatQueryHandler(ISeatService seatService, IMapper mapper, IReservationService reservationService)
        {
            _seatService = seatService;
            _mapper = mapper;
            _reservationService = reservationService;
        }
        #endregion
        public async Task<Response<List<GetAllSeatsResponse>>> Handle(GetAllSeatsQuery request, CancellationToken cancellationToken)
        {
            var seatsList = await _seatService.GetAllAsync();

            var mappedSeatsList = _mapper.Map<List<GetAllSeatsResponse>>(seatsList);

            return Success(mappedSeatsList);
        }

        public async Task<Response<GetSeatByIdResponse>> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
        {
            var seat = await _seatService.GetByIdAsync(request.SeatId);

            if (seat is null)
                return NotFound<GetSeatByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedSeat = _mapper.Map<GetSeatByIdResponse>(seat);

            return Success(mappedSeat);
        }

        public async Task<Response<List<GetFreeSeatsInShowTimeResponse>>> Handle(GetFreeSeatsInShowTimeQuery request, CancellationToken cancellationToken)
        {
            var hallId = await _seatService.GetAllQueryable()
            .Where(s => s.Reservations.Any(r => r.ShowTime.ShowTimeId == request.ShowTimeId))
            .Select(s => s.Hall.HallId)
            .FirstOrDefaultAsync();

            await _reservationService.DeleteNotCompletedReservations();

            var freeSeatsList = await _seatService.GetAllQueryable()
                .Where(s => !s.Reservations.Any(r => r.ShowTime.ShowTimeId == request.ShowTimeId) &&
                            s.Hall.HallId == hallId)
                .Select(s => new GetFreeSeatsInShowTimeResponse
                {
                    SeatId = s.SeatId,
                    SeatNumber = s.SeatNumber,
                    SeatTypeName = s.SeatType.TypeName
                })
                .ToListAsync();

            return Success(freeSeatsList);
        }
    }
}
