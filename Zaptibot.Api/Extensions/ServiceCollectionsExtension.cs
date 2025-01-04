using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using Zaptibot.Api.Configuration;
using Zaptibot.Api.Sounds.Handlers;
using Zaptibot.Api.Sounds.Repository;
using Zaptibot.Api.Users.Handlers;
using Zaptibot.Api.Users.Repository;
using Zaptibot.SharedLib;

namespace Zaptibot.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddSingleton<SqliteConnection>(
            serviceProvider =>
            {
                string? connectionString = configuration.GetConnectionString("DefaultConnection");
                return new SqliteConnection(connectionString);
            }
        );

        /* Services */

        // Repositories
        services.AddSingleton<ISoundRepository, SoundRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();

        // Handlers
        services.AddSingleton<ICreateUserHandler, CreateUserHandler>();
        services.AddSingleton<IGetUsersHandler, GetUsersHandler>();
        services.AddSingleton<IGetSoundHandler, GetSoundHandler>();
        services.AddSingleton<IAddSoundHandler, AddSoundHandler>();

        // Configurations
        services.AddRouting(
            options => { options.LowercaseUrls = true; }
        );
        services.ConfigureOptions<SoundSettingsConfiguration>();

        // Authorization/Authentication
        services.AddAuthorization();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = JwtTokenSettings.GetSymmetricSecurityKey(),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtTokenSettings.Issuer,
                    ValidAudience = JwtTokenSettings.Audience
                };
            });

        return services;
    }
}