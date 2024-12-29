using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.HallMapping
{
    public partial class HallProfile
    {
        public void GetAllHallsMapping()
        {
            CreateMap<Hall, GetHallByIdResponse>();
        }
    }
}
