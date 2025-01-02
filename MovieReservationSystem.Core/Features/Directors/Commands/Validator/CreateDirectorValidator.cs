using FluentValidation;
using MovieReservationSystem.Core.Features.Directors.Commands.Models;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Validator
{
    public class CreateDirectorValidator : AbstractValidator<CreateDirectorCommand>
    {
        private readonly IDirectorService _directorService;

        public CreateDirectorValidator(IDirectorService directorService)
        {
            _directorService = directorService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(d => d.FirstName)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");

            RuleFor(d => d.Bio)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(2500).WithMessage($"{SharedResourcesKeys.MaxLength} 2500");


            RuleFor(d => d.BirthDate)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .GreaterThan(new DateOnly(1900, 1, 1)).WithMessage($"{SharedResourcesKeys.GreaterThan} 1-1-1900");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(d => d.FirstName).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _directorService.IsExistByNameAsync(key, model.LastName);
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }
}
