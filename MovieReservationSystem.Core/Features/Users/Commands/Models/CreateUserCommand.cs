using MediatR;
using MovieReservationSystem.Core.Features.Users.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class CreateUserCommand : IRequest<Response<GetUserByIdResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
