using FluentValidation;
using MovieReservationSystem.Core.Features.Actors.Commands.Models;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Validator
{
    public class CreateActorValidator : AbstractValidator<CreateActorCommand>
    {
        private readonly IActorService _actorService;

        public CreateActorValidator(IActorService actorService)
        {
            _actorService = actorService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");


            RuleFor(a => a.Bio)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(2500).WithMessage($"{SharedResourcesKeys.MaxLength} 2500");


            RuleFor(a => a.BirthDate)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .GreaterThan(new DateOnly(1900, 1, 1)).WithMessage($"{SharedResourcesKeys.GreaterThan} 1-1-1900");
        }
        private void ApplyCustomValidationRules()
        {

            RuleFor(a => a.FirstName).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _actorService.IsExistByNameAsync(key, model.LastName);
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }
}
