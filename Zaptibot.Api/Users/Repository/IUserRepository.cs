using Zaptibot.Api.Users.Models;

namespace Zaptibot.Api.Users.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task AddUserAsync(string name);
    
    /// <summary>
    /// For development purposes only
    /// </summary>
    Task CreateUsersTableAsync();
}