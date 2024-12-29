using MovieReservationSystem.Core.Features.SharedDTOs;

namespace MovieReservationSystem.Core.Features.Actors.Queries.Results
{
    public class GetAllActorsResponse : PersonResponse
    {
        public int ActorId { get; set; }
    }

}
