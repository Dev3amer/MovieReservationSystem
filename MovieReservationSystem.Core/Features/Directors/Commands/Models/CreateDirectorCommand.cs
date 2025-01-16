using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Models
{
    public class CreateDirectorCommand : IRequest<Response<GetDirectorByIdResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public IFormFile? Image { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }
}
