using MediatR;
using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Halls.Commands.Models
{
    public class EditHallCommand : IRequest<Response<GetHallByIdResponse>>
    {
        public int HallId { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }

        public EditHallCommand(int hallId, string name, int capacity)
        {
            HallId = hallId;
            Name = name.Trim();
            Capacity = capacity;
        }
    }
}
