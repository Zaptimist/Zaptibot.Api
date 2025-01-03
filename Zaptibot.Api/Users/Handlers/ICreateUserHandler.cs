namespace Zaptibot.Api.Users.Handlers;

public interface ICreateUserHandler
{
    Task<bool> HandleAsync(string name);
}