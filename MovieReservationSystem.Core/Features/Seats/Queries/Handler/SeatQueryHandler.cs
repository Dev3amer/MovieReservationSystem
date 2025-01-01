using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Seats.Queries.Models;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Seats.Queries.Handler
{
    public class SeatQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSeatsQuery, Response<List<GetAllSeatsResponse>>>,
        IRequestHandler<GetSeatByIdQuery, Response<GetSeatByIdResponse>>
    {
        #region Fields
        private readonly ISeatService _seatService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatQueryHandler(ISeatService seatService, IMapper mapper)
        {
            _seatService = seatService;
            _mapper = mapper;
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
    }
}
