using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Models;
using MovieReservationSystem.Core.Features.ShowTimes.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.ShowTimes.Queries.Handler
{
    public class ShowTimeQueryHandler : ResponseHandler,
        IRequestHandler<GetAllShowTimesQuery, Response<List<GetAllShowTimesResponse>>>,
        IRequestHandler<GetShowTimeByIdQuery, Response<GetShowTimeByIdResponse>>
    {
        #region Fields
        private readonly IShowTimeService _showTimeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ShowTimeQueryHandler(IShowTimeService showTimeService, IMapper mapper)
        {
            _showTimeService = showTimeService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllShowTimesResponse>>> Handle(GetAllShowTimesQuery request, CancellationToken cancellationToken)
        {
            var showTimesList = await _showTimeService.GetAllAsync();

            var mappedShowTimesList = _mapper.Map<List<GetAllShowTimesResponse>>(showTimesList);

            return Success(mappedShowTimesList);
        }

        public async Task<Response<GetShowTimeByIdResponse>> Handle(GetShowTimeByIdQuery request, CancellationToken cancellationToken)
        {
            var showTime = await _showTimeService.GetByIdAsync(request.ShowTimeId);

            if (showTime is null)
                return NotFound<GetShowTimeByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedShowTime = _mapper.Map<GetShowTimeByIdResponse>(showTime);

            return Success(mappedShowTime);
        }
    }
}
