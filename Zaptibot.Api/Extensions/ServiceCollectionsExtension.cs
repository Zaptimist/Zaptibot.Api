using Microsoft.Data.Sqlite;
using Zaptibot.Api.Sounds.Repository;
using Zaptibot.Api.Users.Handlers;
using Zaptibot.Api.Users.Repository;

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
        
        // Configurations
        services.AddRouting(
            options => { options.LowercaseUrls = true; }
        );

        return services;
    }
}