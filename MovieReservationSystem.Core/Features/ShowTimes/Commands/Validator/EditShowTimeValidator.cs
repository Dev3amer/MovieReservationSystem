using FluentValidation;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.ShowTimes.Commands.Validator
{
    public class EditShowTimeValidator : AbstractValidator<EditShowTimeCommand>
    {
        private readonly IShowTimeService _showTimeService;
        private readonly IMovieService _movieService;
        private readonly IHallService _hallService;
        public EditShowTimeValidator(IShowTimeService showTimeService, IMovieService movieService, IHallService hallService)
        {
            _showTimeService = showTimeService;
            _movieService = movieService;
            _hallService = hallService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(st => st.Day)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage($"{SharedResourcesKeys.GreaterThanOrEqual} {DateOnly.FromDateTime(DateTime.Now)}");

            RuleFor(st => st.StartTime)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull);

            RuleFor(st => st.EndTime)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .GreaterThan(st => st.StartTime);

            RuleFor(st => st.ShowTimePrice)
                .GreaterThanOrEqualTo(0).WithMessage($"{SharedResourcesKeys.GreaterThanOrEqual} 0");

            RuleFor(st => st.HallId)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull);

            RuleFor(st => st.MovieId)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull);
        }
        private void ApplyCustomValidationRules()
        {
            //Check if Show Time Id is Vaild
            RuleFor(st => st.ShowTimeId)
                .MustAsync(async (key, CancellationToken) => await _showTimeService.IsExistAsync(key))
                .WithMessage(SharedResourcesKeys.Invalid);


            RuleFor(st => st.StartTime).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _showTimeService.IsExistInSameHallExcludeItselfAsync(model.ShowTimeId, model.HallId, key, model.EndTime);
            }).WithMessage(SharedResourcesKeys.Exist);


            //Check if Movie Exist
            RuleFor(st => st.MovieId).MustAsync(async (key, CancellationToken) =>
            {
                return await _movieService.IsExistAsync(key);
            }).WithMessage(SharedResourcesKeys.Invalid);

            //Check if Hall Exist
            RuleFor(st => st.HallId).MustAsync(async (key, CancellationToken) =>
            {
                return await _hallService.IsExistAsync(key);
            }).WithMessage(SharedResourcesKeys.Invalid);
        }

    }
}
