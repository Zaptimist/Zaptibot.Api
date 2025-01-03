using Zaptibot.Api.Sounds.Repository;
using Zaptibot.Api.Users.Repository;

namespace Zaptibot.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task CreateUsersTableAsync(this IApplicationBuilder app)
    {
        using (IServiceScope scope = app.ApplicationServices.CreateScope())
        {
            IUserRepository userService = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            await userService.CreateUsersTableAsync();
        }
    }
    
    public static async Task CreateSoundsTableAsync(this IApplicationBuilder app)
    {
        using (IServiceScope scope = app.ApplicationServices.CreateScope())
        {
            ISoundRepository soundService = scope.ServiceProvider.GetRequiredService<ISoundRepository>();
            await soundService.CreateSoundsTableAsync();
        }
    }
}