using FluentValidation;
using MovieReservationSystem.Core.Features.Genres.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Validators
{
    public class EditGenreValidator : AbstractValidator<EditGenreCommand>
    {
        private readonly IGenreService _genreService;

        public EditGenreValidator(IGenreService genreService)
        {
            _genreService = genreService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(g => g.GenreId)
               .GreaterThan(Convert.ToByte(0))
               .WithMessage($"{SharedResourcesKeys.GreaterThan} 0");

            RuleFor(g => g.Name)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(55).WithMessage($"{SharedResourcesKeys.MaxLength} 55");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(g => g.GenreId)
                .MustAsync(async (key, CancellationToken) => await _genreService.IsExistAsync(key))
                .WithMessage(SharedResourcesKeys.Invalid);

            RuleFor(g => g.Name).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _genreService.IsExistByNameExcludeItselfAsync(model.GenreId, key);
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }
}
