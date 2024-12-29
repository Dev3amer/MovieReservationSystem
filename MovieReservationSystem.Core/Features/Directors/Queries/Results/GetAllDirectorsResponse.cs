using MovieReservationSystem.Core.Features.SharedDTOs;

namespace MovieReservationSystem.Core.Features.Directors.Queries.Results
{
    public class GetAllDirectorsResponse : PersonResponse
    {
        public int DirectorId { get; set; }
    }
}
