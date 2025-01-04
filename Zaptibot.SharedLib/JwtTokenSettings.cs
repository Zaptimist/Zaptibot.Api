using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Zaptibot.SharedLib
{
    // NOTE: Don't take this for granted. For development purposes, JwtTokenSettings in the SharedLib project is used, but eventually we use for ex. appsettings or dotenv
    public static class JwtTokenSettings
    {
        private static string Key { get; } = "ZaptibotSuperExtremeSecretKeyForNow";
        public static string Issuer { get; } = "Zaptibot";
        public static string Audience { get; } = "Zaptibot";
        public static int AccessTokenExpiration { get; } = 60;
        public static int RefreshTokenExpiration { get; } = 60;
        
        // generate SymmetricSecurityKey
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
    }
}