using MediatR;
using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Halls.Commands.Models
{
    public class CreateHallCommand : IRequest<Response<GetHallByIdResponse>>
    {
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }

        public CreateHallCommand(string name, int capacity)
        {
            Name = name.Trim();
            Capacity = capacity;
        }
    }
}
