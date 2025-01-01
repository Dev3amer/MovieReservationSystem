using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Reservations.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Reservations.Commands.Validators
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;
        private readonly IShowTimeService _showTimeService;
        private readonly ISeatService _seatService;
        public CreateReservationValidator(IReservationService reservationService, UserManager<User> userManager, IShowTimeService showTimeService, ISeatService seatService)
        {
            _reservationService = reservationService;
            _userManager = userManager;
            _showTimeService = showTimeService;
            _seatService = seatService;
            ApplyCustomRules();
        }

        private void ApplyCustomRules()
        {
            //Check If User Is Exist 
            RuleFor(r => r.UserId)
                .MustAsync(async (key, CancellationToken) => await _userManager.FindByIdAsync(key) is not null)
                .WithMessage(SharedResourcesKeys.Invalid);

            //Check If ShowTime Is Exist 
            RuleFor(r => r.ShowTimeId)
                .MustAsync(async (key, CancellationToken) => await _showTimeService.IsExistAndInFutureAsync(key))
                .WithMessage(SharedResourcesKeys.EndedShowTime);

            //Check if Seats is Exist & Exist in the same Show Time Hall
            RuleForEach(r => r.SeatIds).MustAsync(async (model, key, CancellationToken) =>
            {
                var hallId = _showTimeService.GetByIdAsync(model.ShowTimeId).Result.Hall.HallId;
                return await _seatService.IsExistBySeatIdInHallAsync(key, hallId);

            }).WithMessage(SharedResourcesKeys.Invalid);

            //Check if Seats is Exist and Check of its availability
            RuleForEach(r => r.SeatIds).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _reservationService.IsSeatReservedInSameShowTimeAsync(key, model.ShowTimeId);

            }).WithMessage(SharedResourcesKeys.SeatReserved);
        }
    }
}
