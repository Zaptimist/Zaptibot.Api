using Zaptibot.Api.Users.Repository;

namespace Zaptibot.Api.Users.Handlers;

public class CreateUserHandler : ICreateUserHandler
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> HandleAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("User name cannot be empty or whitespace.", nameof(name));
        }

        await _userRepository.AddUserAsync(name);
        return true;
    }
}