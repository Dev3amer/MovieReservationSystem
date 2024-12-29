using MovieReservationSystem.Core.Features.Halls.Commands.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Commands.Models;
using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Core.Mapping.HallMapping
{
    public partial class HallProfile
    {
        public void EditHallMapping()
        {
            CreateMap<EditHallCommand, Hall>()
                .ForMember(h => h.ShowTimes, options => options.Ignore())
                .ForMember(h => h.Seats, options => options.Ignore());
        }
    }
}
