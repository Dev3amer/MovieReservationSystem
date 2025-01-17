using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Reservations.Queries.Models;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared;
using MovieReservationSystem.Core.Pagination;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Reservations.Queries.Handler
{
    internal class ReservationQueriesHandler : ResponseHandler,
        IRequestHandler<GetReservationsPaginatedListQuery, PaginatedList<GetReservationsPaginatedListResponse>>,
        IRequestHandler<GetReservationByIdQuery, Response<GetReservationByIdResponse>>
    {
        #region Fields
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ReservationQueriesHandler(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetReservationByIdResponse>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationService.GetByIdAsync(request.ReservationId);
            if (reservation is null)
                return NotFound<GetReservationByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedReservation = _mapper.Map<GetReservationByIdResponse>(reservation);

            return Success(mappedReservation);
        }

        public async Task<PaginatedList<GetReservationsPaginatedListResponse>> Handle(GetReservationsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var reservationsListQueryable = _reservationService.GetAllQueryable(request.Search);
            //Console.WriteLine(reservationsListQueryable.FirstOrDefault().ReservationDate.Date);
            var PaginatedList = await reservationsListQueryable
                .Select(r => new GetReservationsPaginatedListResponse
                {
                    ReservationId = r.ReservationId,
                    ReservationDate = r.CreatedAt,
                    HallName = r.ShowTime.Hall.Name,
                    Seats = r.ReservedSeats.Select(rs =>
                            new SeatsInReservationResponse { SeatId = rs.SeatId, SeatNumber = rs.SeatNumber }),
                    User = new UserInReservationResponse { Id = r.User.Id, UserName = r.User.UserName },

                    ShowTime = new ShowTimeInReservationResponse
                    {
                        ShowTimeId = r.ShowTime.ShowTimeId,
                        Day = r.ShowTime.Day,
                        StartTime = r.ShowTime.StartTime,
                        EndTime = r.ShowTime.EndTime,
                    }
                }).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedList;
        }
        #endregion
    }
}
