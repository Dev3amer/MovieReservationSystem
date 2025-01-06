using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Service.Abstracts;
using MovieReservationSystem.Service.Implementations;

namespace MovieReservationSystem.Service
{
    public static class ModuleServicesDependencies
    {
        public static void AddModuleServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<ISeatTypeService, SeatTypeService>();
            services.AddTransient<IHallService, HallService>();
            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IShowTimeService, ShowTimeService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
        }
    }
}
