using FluentValidation;
using MovieReservationSystem.Core.Features.Halls.Commands.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Halls.Commands.Validator
{
    public class EditHallValidator : AbstractValidator<EditHallCommand>
    {
        private readonly IHallService _hallService;

        public EditHallValidator(IHallService hallService)
        {
            _hallService = hallService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(h => h.Name)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");

            RuleFor(st => st.Capacity)
                .GreaterThan(0).WithMessage($"{SharedResourcesKeys.GreaterThan} 0");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(h => h.HallId)
                .MustAsync(async (key, CancellationToken) => await _hallService.IsExistAsync(key))
                .WithMessage(SharedResourcesKeys.Invalid);

            RuleFor(h => h.Name).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _hallService.IsExistByNameExcludeItselfAsync(model.HallId, key);
            }).WithMessage(SharedResourcesKeys.Exist);
        }

    }
}
