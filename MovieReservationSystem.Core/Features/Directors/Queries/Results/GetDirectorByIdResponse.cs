using MovieReservationSystem.Core.Features.SharedDTOs;

namespace MovieReservationSystem.Core.Features.Directors.Queries.Results
{
    public class GetDirectorByIdResponse : PersonResponse
    {
        public int DirectorId { get; set; }
    }
}
