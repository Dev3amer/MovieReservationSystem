using MediatR;
using MovieReservationSystem.Core.Features.Actors.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Models
{
    public class CreateActorCommand : IRequest<Response<GetActorByIdResponse>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;

        public CreateActorCommand(string firstName, string lastName, string? imageURL, DateOnly birthDate, string bio)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            ImageURL = imageURL;
            BirthDate = birthDate;
            Bio = bio.Trim();
        }
    }
}
