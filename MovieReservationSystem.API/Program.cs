using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Core;
using MovieReservationSystem.Core.Middleware;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Infrastructure;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Service;

namespace MovieReservationSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DI
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                //Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                //Lockout Settings
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddModuleInfrastructureDependencies();
            builder.Services.AddModuleServicesDependencies();
            builder.Services.AddModuleCoreDependencies();

            #endregion

            #region CORS
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy.AllowAnyOrigin()
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                                      });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
