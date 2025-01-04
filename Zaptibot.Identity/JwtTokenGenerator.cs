using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Zaptibot.Identity;

public class JwtTokenGenerator(IOptions<JwtTokenSettings> options)
{
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
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteArray), SecurityAlgorithms.HmacSha256Signature),
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience
        };
        
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}