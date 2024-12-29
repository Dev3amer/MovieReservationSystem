using MediatR;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Pagination;
using MovieReservationSystem.Data.Helpers;

namespace MovieReservationSystem.Core.Features.Movies.Queries.Models
{
    public class GetMoviesPaginatedListQuery : IRequest<PaginatedList<GetMoviesPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public MovieOrderingEnum? MovieOrdering { get; set; }
        public GetMoviesPaginatedListQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
