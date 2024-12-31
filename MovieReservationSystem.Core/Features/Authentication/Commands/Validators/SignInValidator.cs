using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Authentication.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public SignInValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion


        private void ApplyValidationRules()
        {
            RuleFor(s => s.UserName)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty);

            RuleFor(s => s.Password)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty);
        }
        private void ApplyCustomValidationRules()
        {

        }
    }
}
