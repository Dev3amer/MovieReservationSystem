using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string UserId { get; set; } = default!;
        public string Code { get; set; } = default!;
    }
}
