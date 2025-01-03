using Microsoft.AspNetCore.Mvc;
using Zaptibot.Api.Users.Handlers;
using Zaptibot.Api.Users.Models;

namespace Zaptibot.Api.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICreateUserHandler _createUserHandler;
    private readonly IGetUsersHandler _getUsersHandler;

    public UsersController(
        ICreateUserHandler createUserHandler,
        IGetUsersHandler getUsersHandler
    )
    {
        _createUserHandler = createUserHandler ?? throw new ArgumentNullException(nameof(createUserHandler));
        _getUsersHandler = getUsersHandler ?? throw new ArgumentNullException(nameof(getUsersHandler));
    }

    [HttpPost]
    [Route("/add-user")]
    public async Task<IActionResult> AddUser([FromQuery] string name)
    {
        await _createUserHandler.HandleAsync(name);
        return Ok();
    }

    [HttpGet]
    [Route("/users")]
    public async Task<IActionResult> GetUsers()
    {
        IEnumerable<User> users = await _getUsersHandler.GetAllUsersAsync();
        return Ok(users);
    }
}