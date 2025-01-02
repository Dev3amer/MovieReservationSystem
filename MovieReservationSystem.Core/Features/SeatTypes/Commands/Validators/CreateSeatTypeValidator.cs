using FluentValidation;
using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Validators
{
    public class CreateSeatTypeValidator : AbstractValidator<CreateSeatTypeCommand>
    {
        private readonly ISeatTypeService _seatTypeService;

        public CreateSeatTypeValidator(ISeatTypeService seatTypeService)
        {
            _seatTypeService = seatTypeService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(st => st.TypeName)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");

            RuleFor(st => st.SeatTypePrice)
                .GreaterThanOrEqualTo(0).WithMessage($"{SharedResourcesKeys.GreaterThanOrEqual} 0");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(st => st.TypeName).MustAsync(async (key, CancellationToken) =>
            {
                return !await _seatTypeService.IsExistByNameAsync(key);
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }
}
