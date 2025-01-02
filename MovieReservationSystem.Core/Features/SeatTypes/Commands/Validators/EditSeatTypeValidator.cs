using FluentValidation;
using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Validators
{
    public class EditSeatTypeValidator : AbstractValidator<EditSeatTypeCommand>
    {
        private readonly ISeatTypeService _seatTypeService;

        public EditSeatTypeValidator(ISeatTypeService seatTypeService)
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
                .GreaterThanOrEqualTo(0).WithMessage($"{SharedResourcesKeys.GreaterThan} 0");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(st => st.SeatTypeId)
                .MustAsync(async (key, CancellationToken) => await _seatTypeService.IsExistAsync(key))
                .WithMessage(SharedResourcesKeys.Invalid);

            RuleFor(st => st.TypeName).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _seatTypeService.IsExistByNameExcludeItselfAsync(model.SeatTypeId, key);
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }
}
