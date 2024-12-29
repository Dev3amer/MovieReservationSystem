using MediatR;
using MovieReservationSystem.Core.Features.Actors.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Models
{
    public class EditActorCommand : IRequest<Response<GetActorByIdResponse>>
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;

        public EditActorCommand(int actorId, string firstName, string lastName, string? imageURL, DateOnly birthDate, string bio)
        {
            ActorId = actorId;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            ImageURL = imageURL;
            BirthDate = birthDate;
            Bio = bio;
        }
    }
}
