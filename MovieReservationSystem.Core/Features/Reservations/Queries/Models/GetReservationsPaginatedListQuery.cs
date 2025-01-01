using MediatR;
using MovieReservationSystem.Core.Features.Reservations.Queries.Results;
using MovieReservationSystem.Core.Pagination;

namespace MovieReservationSystem.Core.Features.Reservations.Queries.Models
{
    public class GetReservationsPaginatedListQuery : IRequest<PaginatedList<GetReservationsPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateOnly? Search { get; set; }
        public GetReservationsPaginatedListQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
