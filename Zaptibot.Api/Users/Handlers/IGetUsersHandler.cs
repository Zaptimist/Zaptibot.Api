using Zaptibot.Api.Users.Models;

namespace Zaptibot.Api.Users.Handlers;

public interface IGetUsersHandler
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetSingleUserAsync(int id);
}