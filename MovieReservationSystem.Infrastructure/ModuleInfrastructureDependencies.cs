using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Implementations;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static void AddModuleInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<ISeatTypeRepository, SeatTypeRepository>();
            services.AddTransient<IHallRepository, HallRepository>();
            services.AddTransient<IDirectorRepository, DirectorRepository>();
            services.AddTransient<IActorRepository, ActorRepository>();
            services.AddTransient<ISeatRepository, SeatRepository>();
            services.AddTransient<IShowTimeRepository, ShowTimeRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
        }
    }
}
