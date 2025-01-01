using AutoMapper;
using MediatR;
using MovieReservationSystem.Core.Features.Halls.Commands.Models;
using MovieReservationSystem.Core.Features.Halls.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Halls.Commands.Handler
{
    public class SeatCommandsHandler : ResponseHandler,
        IRequestHandler<CreateHallCommand, Response<GetHallByIdResponse>>,
        IRequestHandler<EditHallCommand, Response<GetHallByIdResponse>>,
        IRequestHandler<DeleteHallCommand, Response<bool>>
    {
        #region Fields
        private readonly IHallService _hallService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatCommandsHandler(IHallService hallService, IMapper mapper)
        {
            _hallService = hallService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<GetHallByIdResponse>> Handle(CreateHallCommand request, CancellationToken cancellationToken)
        {
            var hall = _mapper.Map<Hall>(request);
            var savedHall = await _hallService.AddAsync(hall);
            var response = _mapper.Map<GetHallByIdResponse>(savedHall);
            return Success(response);
        }
        public async Task<Response<GetHallByIdResponse>> Handle(EditHallCommand request, CancellationToken cancellationToken)
        {
            var oldHall = await _hallService.GetByIdAsync(request.HallId);
            var mappedHall = _mapper.Map(request, oldHall);
            var savedHall = await _hallService.EditAsync(mappedHall);
            var response = _mapper.Map<GetHallByIdResponse>(savedHall);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.GetByIdAsync(request.HallId);

            var isDeleted = await _hallService.DeleteAsync(hall);
            return isDeleted ? Deleted<bool>() : BadRequest<bool>();
        }
    }
}
