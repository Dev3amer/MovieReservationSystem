using FluentValidation;
using MovieReservationSystem.Core.Features.Seats.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Seats.Commands.Validator
{
    public class CreateSeatValidator : AbstractValidator<CreateSeatCommand>
    {
        private readonly ISeatService _seatService;
        private readonly IHallService _hallService;
        private readonly ISeatTypeService _seatTypeService;

        public CreateSeatValidator(ISeatService seatService, IHallService hallService, ISeatTypeService seatTypeService)
        {
            _seatService = seatService;
            _hallService = hallService;
            _seatTypeService = seatTypeService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(s => s.SeatNumber)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");
        }
        private void ApplyCustomValidationRules()
        {

            RuleFor(s => s.SeatNumber).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _seatService.IsExistInHallAsync(key, model.HallId);
            }).WithMessage(SharedResourcesKeys.Exist);

            //Check if Hall is Not Exist
            RuleFor(s => s.HallId).MustAsync(async (key, CancellationToken) =>
            {
                return await _hallService.IsExistAsync(key);
            }).WithMessage(SharedResourcesKeys.Invalid);

            //Check if SeatType is Not Exist
            RuleFor(s => s.SeatTypeId).MustAsync(async (key, CancellationToken) =>
            {
                return await _seatTypeService.IsExistAsync(key);
            }).WithMessage(SharedResourcesKeys.Invalid);


        }
    }
}
