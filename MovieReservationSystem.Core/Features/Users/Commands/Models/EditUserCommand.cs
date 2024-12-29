using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
