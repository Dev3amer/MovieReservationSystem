using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Directors.Commands.Models;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Core.Features.Directors.Commands.Handler
{
    public class DirectorCommandHandler : ResponseHandler,
        IRequestHandler<CreateDirectorCommand, Response<GetDirectorByIdResponse>>,
        IRequestHandler<EditDirectorCommand, Response<GetDirectorByIdResponse>>,
        IRequestHandler<DeleteDirectorCommand, Response<bool>>
    {
        #region Fields
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region Constructors
        public DirectorCommandHandler(IDirectorService directorService, IMapper mapper, IFileService fileService, IHttpContextAccessor contextAccessor)
        {
            _directorService = directorService;
            _mapper = mapper;
            _fileService = fileService;
            _contextAccessor = contextAccessor;
        }
        #endregion
        public async Task<Response<GetDirectorByIdResponse>> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            request.FirstName = request.FirstName.Trim();
            request.LastName = request.LastName.Trim();

            var director = _mapper.Map<Director>(request);

            var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
            try
            {
                if (request.Image is null)
                    director.Person.ImageURL = baseURL + _fileService.GetDefaultImagePath();
                else
                    director.Person.ImageURL = baseURL + await _fileService.SaveImageAsync(request.Image, "directors");
            }
            catch (Exception ex)
            {
                return BadRequest<GetDirectorByIdResponse>(ex.Message);
            }

            var savedDirector = await _directorService.AddAsync(director);
            var response = _mapper.Map<GetDirectorByIdResponse>(savedDirector);
            return Success(response);
        }
        public async Task<Response<GetDirectorByIdResponse>> Handle(EditDirectorCommand request, CancellationToken cancellationToken)
        {
            request.FirstName = request.FirstName.Trim();
            request.LastName = request.LastName.Trim();

            var oldDirector = await _directorService.GetByIdAsync(request.DirectorId);
            var oldImage = oldDirector.Person.ImageURL;

            var mappedDirector = _mapper.Map(request, oldDirector);

            var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
            var oldImagePath = oldImage.Remove(0, baseURL.Length);

            try
            {
                if (request.Image is not null)
                {
                    mappedDirector.Person.ImageURL = baseURL + await _fileService.ReplaceImageAsync(oldImagePath, request.Image, "directors");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<GetDirectorByIdResponse>(ex.Message);
            }

            var savedDirector = await _directorService.EditAsync(mappedDirector);
            var response = _mapper.Map<GetDirectorByIdResponse>(savedDirector);
            return Success(response);
        }

        public async Task<Response<bool>> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _directorService.GetByIdAsync(request.DirectorId);

            var isDeleted = await _directorService.DeleteAsync(director);
            if (isDeleted)
            {
                var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + "/";
                _fileService.DeleteImage(director.Person.ImageURL.Remove(0, baseURL.Length));
                return Deleted<bool>();
            }
            return BadRequest<bool>();
        }
    }
}
