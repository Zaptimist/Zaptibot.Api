using Zaptibot.Api.Users.Models;
using Zaptibot.Api.Users.Repository;

namespace Zaptibot.Api.Users.Handlers;

public class GetUsersHandler : IGetUsersHandler
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task<User?> GetSingleUserAsync(int id)
    {
        var users = await _userRepository.GetUsersAsync();
        return users.FirstOrDefault(user => user.Id == id);
    }
}