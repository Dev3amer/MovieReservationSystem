using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Actors.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Models
{
    public class CreateActorCommand : IRequest<Response<GetActorByIdResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public IFormFile? Image { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }
}
