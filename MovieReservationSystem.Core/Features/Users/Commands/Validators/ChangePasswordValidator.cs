using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Users.Commands.Models;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Core.Features.Users.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public ChangePasswordValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion


        private void ApplyValidationRules()
        {
            RuleFor(u => u.ConfirmPassword)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .Equal(u => u.Password).WithMessage($"{SharedResourcesKeys.InvalidConfirmPassword}");
        }
        private void ApplyCustomValidationRules()
        {
            //Check if user is Exist by Id
            RuleFor(u => u.Id).MustAsync(async (key, CancellationToken) =>
            {
                return await _userManager.FindByIdAsync(key) is not null;
            }).WithMessage(SharedResourcesKeys.Invalid);
        }
    }
}
