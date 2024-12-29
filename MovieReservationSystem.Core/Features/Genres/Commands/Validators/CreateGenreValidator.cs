using FluentValidation;
using MovieReservationSystem.Core.Features.Genres.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Validators
{
    public class CreateGenreValidator : AbstractValidator<CreateGenreCommand>
    {
        private readonly IGenreService _genreService;

        public CreateGenreValidator(IGenreService genreService)
        {
            _genreService = genreService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(g => g.Name).MustAsync(async (key, CancellationToken) =>
            {
                return !await _genreService.IsExistByNameAsync(key);
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }
}
