using Microsoft.Extensions.Options;

namespace Zaptibot.Identity.Configuration;

public class JwtTokenSettingsConfiguration : IConfigureOptions<JwtTokenSettings>
{
    private readonly IConfiguration _configuration;

    public JwtTokenSettingsConfiguration(IConfiguration configuration)
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