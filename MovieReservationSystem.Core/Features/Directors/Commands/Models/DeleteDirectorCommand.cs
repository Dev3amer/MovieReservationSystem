using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Models
{
    public class DeleteDirectorCommand : IRequest<Response<bool>>
    {
        public int DirectorId { get; set; }
    }
}
