using FluentValidation;
using MovieReservationSystem.Core.Features.Authorization.Commands.Models;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Authorization.Commands.Validator
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public DeleteRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        private void ApplyValidationRules()
        {
            RuleFor(r => r.RoleId)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(256).WithMessage($"{SharedResourcesKeys.MaxLength} 450");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(r => r.RoleId).MustAsync(async (key, CancellationToken) =>
            {
                return await _authorizationService.IsRoleExistByIdAsync(key);
            }).WithMessage(SharedResourcesKeys.Invalid);
        }
        #endregion
    }
}
