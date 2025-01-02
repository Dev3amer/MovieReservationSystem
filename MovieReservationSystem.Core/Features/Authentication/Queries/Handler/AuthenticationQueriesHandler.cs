using MediatR;
using MovieReservationSystem.Core.Features.Authentication.Queries.Models;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Authentication.Queries.Handler
{
    public class AuthenticationQueriesHandler : ResponseHandler,
        IRequestHandler<GetAccessTokenValidityQuery, Response<string>>
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors
        public AuthenticationQueriesHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        #endregion

        public async Task<Response<string>> Handle(GetAccessTokenValidityQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateAccessTokenAsync(request.AccessToken);
            if (result != null)
                return Unauthorized<string>(result);
            return Success("Valid Token");
        }
    }
}
