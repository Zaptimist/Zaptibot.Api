using Zaptibot.Api.Sounds.Models.Dto;
using Zaptibot.Api.Sounds.Models.Entity;

namespace Zaptibot.Api.Sounds.Extensions;

public static class SoundExtension
{
    // TODO: Fix this extension method
    public static SoundDto ToDto(this Sound sound)
    {
        return new SoundDto
        {
            // Id = sound.Id,
            Name = sound.Name,
            // Path = sound.Path,
        };
    }
    
    // TODO: Fix this extension method
    public static Sound ToEntity(this SoundDto soundDto)
    {
        return new Sound
        {
            // Id = soundDto.Id,
            Name = soundDto.Name,
            // Path = soundDto.Path,
        };
    }
}