using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared;
using MovieReservationSystem.Core.Features.Users.Queries.Models;
using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Users.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler,
       IRequestHandler<GetAllUsersQuery, Response<List<GetAllUsersResponse>>>,
       IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<GetUserReservationsHistoryQuery, Response<List<GetUserReservationsHistoryResponse>>>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;
        #endregion

        #region Constructors
        public UserQueryHandler(IMapper mapper, UserManager<User> userManager, IUserService userService, IReservationService reservationService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
            _reservationService = reservationService;
        }
        #endregion
        public async Task<Response<List<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _userManager.Users.Select(u => new GetAllUsersResponse
            {
                Id = u.Id,
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName
            }).ToListAsync();

            return Success(usersList);
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var userMappedIntoResponse = await _userManager.Users.Where(u => u.Id == request.Id)
            .Select(u => new GetUserByIdResponse
            {
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName
            }).FirstOrDefaultAsync();

            if (userMappedIntoResponse is null)
                return NotFound<GetUserByIdResponse>(SharedResourcesKeys.NotFound);

            return Success(userMappedIntoResponse);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.ConfirmUserEmail(await _userManager.FindByIdAsync(request.UserId), request.Code);
                return Success(SharedResourcesKeys.EmailConfirmed);

            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<List<GetUserReservationsHistoryResponse>>> Handle(GetUserReservationsHistoryQuery request, CancellationToken cancellationToken)
        {
            var userReservations = await _reservationService.GetAllQueryable()
                .Where(r => r.UserId == request.Id).ToListAsync();



            var response = userReservations
                .Select(ur => new GetUserReservationsHistoryResponse
                {
                    ReservationId = ur.ReservationId,
                    FinalPrice = ur.FinalPrice,
                    ReservationDate = ur.CreatedAt,
                    HallName = ur.ShowTime.Hall.Name,
                    PaymentStatus = ur.PaymentStatus.ToString(),
                    Seats = ur.ReservedSeats.Select(urs => new SeatsInReservationResponse
                    {
                        SeatId = urs.SeatId,
                        SeatNumber = urs.SeatNumber,
                    }),
                    ShowTime = new ShowTimeInReservationResponse
                    {
                        ShowTimeId = ur.ShowTime.ShowTimeId,
                        MovieName = ur.ShowTime.Movie.Title,
                        Day = ur.ShowTime.Day,
                        StartTime = ur.ShowTime.StartTime,
                        EndTime = ur.ShowTime.EndTime
                    }
                }).ToList();
            return Success(response);
        }
    }
}
