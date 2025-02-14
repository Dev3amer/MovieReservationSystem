﻿using MediatR;
using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Genres.Queries.Models
{
    public class GetAllGenresQuery : IRequest<Response<List<GetAllGenresResponse>>>
    {
    }
}
