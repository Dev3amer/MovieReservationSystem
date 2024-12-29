using MediatR;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Models
{
    public class CreateDirectorCommand : IRequest<Response<GetDirectorByIdResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;

        public CreateDirectorCommand(string firstName, string lastName, string? imageURL, DateOnly birthDate, string bio)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            ImageURL = imageURL;
            BirthDate = birthDate;
            Bio = bio;
        }
    }
}
