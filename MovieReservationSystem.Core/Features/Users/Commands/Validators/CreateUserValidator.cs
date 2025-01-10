using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Core.Features.Users.Commands.Models;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Resources;

namespace MovieReservationSystem.Core.Features.Users.Commands.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public CreateUserValidator(UserManager<User> userManager)
        {
            _userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion


        private void ApplyValidationRules()
        {
            RuleFor(u => u.FirstName)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .MaximumLength(256).WithMessage($"{SharedResourcesKeys.MaxLength} 256");

            RuleFor(u => u.LastName)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .MaximumLength(256).WithMessage($"{SharedResourcesKeys.MaxLength} 256");

            RuleFor(u => u.UserName)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .MaximumLength(256).WithMessage($"{SharedResourcesKeys.MaxLength} 256");

            RuleFor(u => u.Email)
            .NotNull().WithMessage(SharedResourcesKeys.NotNull)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage($"{SharedResourcesKeys.InvalidEmail}");


            RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage(SharedResourcesKeys.NotNull)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .Matches(@"^(?:\+?20|0020|0)?1[0125]\d{8}$").WithMessage($"{SharedResourcesKeys.InvalidPhone}");



            RuleFor(u => u.ConfirmPassword)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .Equal(u => u.Password).WithMessage($"{SharedResourcesKeys.InvalidConfirmPassword}");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(u => u.UserName).MustAsync(async (key, CancellationToken) =>
            {
                return await _userManager.FindByNameAsync(key) is null;
            }).WithMessage(SharedResourcesKeys.Exist);

            RuleFor(u => u.Email).MustAsync(async (key, CancellationToken) =>
            {
                return await _userManager.FindByEmailAsync(key) is null;
            }).WithMessage(SharedResourcesKeys.Exist);
        }
    }

}
