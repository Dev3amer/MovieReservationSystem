using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Filters
{
    public class ReservationManagerRoleFilter : IAsyncActionFilter
    {
        private readonly ICurrentUserService _currentUserService;

        public ReservationManagerRoleFilter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!await _currentUserService.CheckIfRuleExist("Reservation Manager"))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
