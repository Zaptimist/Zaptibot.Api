using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Zaptibot.Identity;

// FIXME: IOptionsMonitor does not work in DI, For now we keep the options, but also use JwtTokenSettings from the same settings
// TODO: See SharedLib/JwtTokenSettings.cs for more information and fix that
public class JwtTokenGenerator(IOptions<JwtTokenSettings> options)
{
    // create expires access token
    
    public string GenerateToken(string email)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] byteArray = Encoding.UTF8.GetBytes(options.Value.Key);
        
        List<Claim> claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, email),
        };
        
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(options.Value.AccessTokenExpiration),
            SigningCredentials = new SigningCredentials(SharedLib.JwtTokenSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience
        };
        
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}