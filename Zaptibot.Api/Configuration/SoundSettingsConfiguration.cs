using Microsoft.Extensions.Options;
using Zaptibot.Api.Settings;

namespace Zaptibot.Api.Configuration;

public class SoundSettingsConfiguration : IConfigureOptions<SoundSettings>
{
    private readonly IConfiguration _configuration;

    public SoundSettingsConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(SoundSettings options)
    {
        _configuration
            .GetSection(nameof(SoundSettings))
            .Bind(options);
    }
}