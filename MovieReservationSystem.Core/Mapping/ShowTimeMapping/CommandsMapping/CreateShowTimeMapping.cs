using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.ShowTimeMapping
{
    public partial class ShowTimeProfile
    {
        public void CreateShowTimeMapping()
        {
            CreateMap<CreateShowTimeCommand, ShowTime>();
        }
    }
}
