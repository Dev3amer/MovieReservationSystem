using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Models
{
    public class DeleteDirectorCommand : IRequest<Response<bool>>
    {
        public int DirectorId { get; set; }
    }
}
