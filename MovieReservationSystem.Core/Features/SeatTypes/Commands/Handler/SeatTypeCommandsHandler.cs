using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.SeatTypes.Commands.Models;
using MovieReservationSystem.Core.Features.SeatTypes.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Handler
{
    public class SeatTypeCommandsHandler : ResponseHandler,
        IRequestHandler<CreateSeatTypeCommand, Response<GetSeatTypeByIdResponse>>,
        IRequestHandler<EditSeatTypeCommand, Response<GetSeatTypeByIdResponse>>,
        IRequestHandler<DeleteSeatTypeCommand, Response<bool>>
    {
        #region Fields
        private readonly ISeatTypeService _seatService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatTypeCommandsHandler(ISeatTypeService seatService, IMapper mapper)
        {
            _seatService = seatService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<GetSeatTypeByIdResponse>> Handle(CreateSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var seatType = _mapper.Map<SeatType>(request);
            var savedSeatType = await _seatService.AddAsync(seatType);
            var response = _mapper.Map<GetSeatTypeByIdResponse>(savedSeatType);
            return Success(response);
        }
        public async Task<Response<GetSeatTypeByIdResponse>> Handle(EditSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var oldSeatType = await _seatService.GetByIdAsync(request.SeatTypeId);
            var mappedSeatType = _mapper.Map(request, oldSeatType);
            var savedSeatType = await _seatService.EditAsync(mappedSeatType);
            var response = _mapper.Map<GetSeatTypeByIdResponse>(savedSeatType);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var seatType = await _seatService.GetByIdAsync(request.SeatTypeId);

            var isDeleted = await _seatService.DeleteAsync(seatType);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
