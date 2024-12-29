using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Halls.Queries.Models;
using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Halls.Queries.Handler
{
    public class HallQueryHandler : ResponseHandler,
        IRequestHandler<GetAllHallsQuery, Response<List<GetAllHallsResponse>>>,
        IRequestHandler<GetHallByIdQuery, Response<GetHallByIdResponse>>
    {
        #region Fields
        private readonly IHallService _hallService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public HallQueryHandler(IHallService hallService, IMapper mapper)
        {
            _hallService = hallService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllHallsResponse>>> Handle(GetAllHallsQuery request, CancellationToken cancellationToken)
        {
            var hallsList = await _hallService.GetAllAsync();

            var mappedHallsList = _mapper.Map<List<GetAllHallsResponse>>(hallsList);

            return Success(mappedHallsList);
        }

        public async Task<Response<GetHallByIdResponse>> Handle(GetHallByIdQuery request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.GetByIdAsync(request.HallId);

            if (hall is null)
                return NotFound<GetHallByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedHall = _mapper.Map<GetHallByIdResponse>(hall);

            return Success(mappedHall);
        }
    }
}
