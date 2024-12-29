using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Models;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Core.Resources;
using MovieReservationSystem.Core.ResponseBases;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.SeatTypes.Queries.Handler
{
    public class SeatTypeQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSeatTypesQuery, Response<List<GetAllSeatTypesResponse>>>,
        IRequestHandler<GetSeatTypeByIdQuery, Response<GetSeatTypeByIdResponse>>
    {
        #region Fields
        private readonly ISeatTypeService _seatService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatTypeQueryHandler(ISeatTypeService seatService, IMapper mapper)
        {
            _seatService = seatService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllSeatTypesResponse>>> Handle(GetAllSeatTypesQuery request, CancellationToken cancellationToken)
        {
            var seatTypesList = await _seatService.GetAllAsync();

            var mappedSeatTypesList = _mapper.Map<List<GetAllSeatTypesResponse>>(seatTypesList);

            return Success(mappedSeatTypesList);
        }

        public async Task<Response<GetSeatTypeByIdResponse>> Handle(GetSeatTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var SeatType = await _seatService.GetByIdAsync(request.SeatTypeId);

            if (SeatType is null)
                return NotFound<GetSeatTypeByIdResponse>(SharedResourcesKeys.NotFound);

            var mappedSeatType = _mapper.Map<GetSeatTypeByIdResponse>(SeatType);

            return Success(mappedSeatType);
        }
    }
}
