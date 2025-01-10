using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Users.Queries.Models;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Resources;

namespace MovieReservationSystem.Core.Features.Users.Queries.Validator
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public ConfirmEmailValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion


        private void ApplyValidationRules()
        {
            RuleFor(c => c.UserId)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty);

            RuleFor(c => c.Code)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(c => c.UserId).MustAsync(async (key, CancellationToken) =>
            {
                return await _userManager.FindByIdAsync(key) is not null;
            }).WithMessage(SharedResourcesKeys.Invalid);
        }
    }
}
