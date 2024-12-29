using FluentValidation;
using MovieReservationSystem.Core.Features.Halls.Commands.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Halls.Commands.Validator
{
    public class CreateHallValidator : AbstractValidator<CreateHallCommand>
    {
        private readonly IHallService _hallService;

        public CreateHallValidator(IHallService hallService)
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
            RuleFor(h => h.Name).MustAsync(async (key, CancellationToken) =>
            {
                return !await _hallService.IsExistByNameAsync(key);
            }).WithMessage(SharedResourcesKeys.Exist);
        }

    }
}
