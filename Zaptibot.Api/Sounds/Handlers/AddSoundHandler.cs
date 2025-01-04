using Microsoft.Extensions.Options;
using Zaptibot.Api.Settings;
using Zaptibot.Api.Sounds.Models.Entity;
using Zaptibot.Api.Sounds.Repository;

namespace Zaptibot.Api.Sounds.Handlers;

public sealed class AddSoundHandler(
    ISoundRepository soundRepository, 
    IOptions<SoundSettings> options) : IAddSoundHandler
{
    public async Task<Sound> HandleAsync(string name)
    {
        string path = options.Value.Path;
        
        if(path is null)
        {
            throw new InvalidOperationException("Sounds path is not set in the appsettings");
        }
        
        Sound sound = new Sound
        {
            Path = path,
            Name = name,
        };
        
        await soundRepository.AddAsync(sound);
        
        return sound;
    }
}