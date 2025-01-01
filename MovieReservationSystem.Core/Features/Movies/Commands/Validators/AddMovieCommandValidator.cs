using FluentValidation;
using MovieReservationSystem.Core.Features.Movies.Commands.Models;
using MovieReservationSystem.Core.Features.Reservations.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Movies.Commands.Validators
{
    public class AddMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IDirectorService _directorService;
        private readonly IActorService _actorService;
        public AddMovieCommandValidator(IMovieService movieService, IGenreService genreService, IDirectorService directorService, IActorService actorService)
        {
            _movieService = movieService;
            _genreService = genreService;
            _directorService = directorService;
            _actorService = actorService;
            ApplyValidationRules();
            ApplyCustomRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(m => m.Title)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(256).WithMessage($"{SharedResourcesKeys.MaxLength} 256");

            RuleFor(m => m.Description)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(256).WithMessage($"{SharedResourcesKeys.MaxLength} 2500");

            RuleFor(m => m.PosterURL)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull);


            RuleFor(m => m.Rate)
                .InclusiveBetween(0.0m, 9.9m).WithMessage($"{SharedResourcesKeys.AcceptedRange} [0.0 - 9.9]");


            RuleFor(m => m.DurationInMinutes)
                .GreaterThan(0).WithMessage($"{SharedResourcesKeys.GreaterThan} 0");

            RuleFor(m => m.ReleaseYear)
                .GreaterThan(1800).WithMessage($"{SharedResourcesKeys.GreaterThan} 1800");
        }
        private void ApplyCustomRules()
        {
            RuleFor(m => m.Title).MustAsync(async (key, CancellationToken) =>
            {
                return !await _movieService.IsExistByNameAsync(key);
            }).WithMessage(SharedResourcesKeys.Exist);

            //Check If Genre Is not Exist 
            RuleForEach(m => m.GenresIds)
                .MustAsync(async (key, CancellationToken) => await _genreService.IsExistAsync(key))
                .WithMessage(SharedResourcesKeys.Invalid);

            //Check If Actor Is not Exist 
            RuleForEach(m => m.ActorsIds)
                .MustAsync(async (key, CancellationToken) => await _actorService.IsExistAsync(key))
                .WithMessage(SharedResourcesKeys.Invalid);


            //Check if SeatType is Not Exist
            RuleFor(s => s.DirectorId).MustAsync(async (key, CancellationToken) =>
            {
                return await _directorService.IsExistAsync(key);
            }).WithMessage(SharedResourcesKeys.Invalid);

        }
    }
}
