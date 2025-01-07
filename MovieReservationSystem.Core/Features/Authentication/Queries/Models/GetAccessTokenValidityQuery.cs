using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Authentication.Queries.Models
{
    public class GetAccessTokenValidityQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; } = default!;
    }
}