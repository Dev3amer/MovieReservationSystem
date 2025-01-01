using MediatR;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Models
{
    public class EditDirectorCommand : IRequest<Response<GetDirectorByIdResponse>>
    {
        public int DirectorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;

        public EditDirectorCommand(int directorId, string firstName, string lastName, string? imageURL, DateOnly birthDate, string bio)
        {
            DirectorId = directorId;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            ImageURL = imageURL;
            BirthDate = birthDate;
            Bio = bio;
        }
    }
}
