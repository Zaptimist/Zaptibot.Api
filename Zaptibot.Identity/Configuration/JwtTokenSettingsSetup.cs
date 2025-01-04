using Microsoft.Extensions.Options;

namespace Zaptibot.Identity;

public class JwtTokenSettingsSetup : IConfigureOptions<JwtTokenSettings>
{
    private readonly IConfiguration _configuration;

    public JwtTokenSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtTokenSettings options)
    {
        _configuration
            .GetSection(nameof(JwtTokenSettings))
            .Bind(options);
    }
}