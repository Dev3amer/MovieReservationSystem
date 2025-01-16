using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieReservationSystem.Core;
using MovieReservationSystem.Core.Filters;
using MovieReservationSystem.Core.Middleware;
using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Helpers;
using MovieReservationSystem.Infrastructure;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.Seeding;
using MovieReservationSystem.Service;
using MovieReservationSystem.Service.Abstracts;
using MovieReservationSystem.Service.Implementations;
using System.Text;

namespace MovieReservationSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Swagger
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Swagger Gn
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Reservation System", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }
           });
            });

            //builder.Services.AddAuthorization(option =>
            //{
            //    option.AddPolicy("CreateStudent", policy =>
            //    {
            //        policy.RequireClaim("Create Student", "True");
            //    });
            //    option.AddPolicy("DeleteStudent", policy =>
            //    {
            //        policy.RequireClaim("Delete Student", "True");
            //    });
            //    option.AddPolicy("EditStudent", policy =>
            //    {
            //        policy.RequireClaim("Edit Student", "True");
            //    });
            //});
            #endregion

            #region DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Identity
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
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region Modules Interfaces

            builder.Services.AddModuleInfrastructureDependencies();
            builder.Services.AddModuleServicesDependencies();
            builder.Services.AddModuleCoreDependencies();
            #endregion

            #region Authentication
            var jwtSettings = new JwtSettings();
            builder.Configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            builder.Services.AddSingleton(jwtSettings);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = jwtSettings.ValidateIssuer,
                   ValidIssuers = new[] { jwtSettings.Issuer },
                   ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                   ValidAudience = jwtSettings.Audience,
                   ValidateAudience = jwtSettings.ValidateAudience,
                   ValidateLifetime = jwtSettings.ValidateLifeTime,
               };
           });
            #endregion

            #region Emails Settings
            var smtpSettings = new SmtpSettings();
            builder.Configuration.GetSection("SMTP").Bind(smtpSettings);
            builder.Services.AddSingleton(smtpSettings);
            #endregion

            #region UrlHelper
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
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

            #region Filters
            builder.Services.AddTransient<DataEntryRoleFilter>();
            builder.Services.AddTransient<ReservationManagerRoleFilter>();
            #endregion

            #region File Service
            builder.Services.AddScoped<IFileService>(servicesProvider =>
            {
                var webHostEnvironment = servicesProvider.GetRequiredService<IWebHostEnvironment>();
                return new FileService(webHostEnvironment.WebRootPath);
            });
            #endregion

            var app = builder.Build();

            #region Seeders
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await RolesSeeder.SeedRolesAsync(roleManager);
                await UserSeeder.SeedUserAsync(userManager);
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
