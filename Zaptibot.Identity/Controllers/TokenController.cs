using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Zaptibot.Identity.Controllers;

public class TokenController : ControllerBase
{
    private readonly JwtTokenGenerator _tokenGenerator;

    public TokenController(JwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("/login")]
    public object Login([FromBody] LoginRequest request)
    {
        return new
        {
            access_token = _tokenGenerator.GenerateToken(request.Email)
        };
    }
    
}