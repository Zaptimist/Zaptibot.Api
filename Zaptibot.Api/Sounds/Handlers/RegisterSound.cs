using Zaptibot.Api.Sounds.Repository;

namespace Zaptibot.Api.Sounds.Handlers;

public sealed class RegisterSound(ISoundRepository soundRepository)
{
    public record Request(string Name, string Path);

    public async Task<Sound> HandleAsync(Request request)
    {
        Sound newSound = new Sound
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Path = request.Path
        };
        
        await soundRepository.AddAsync(newSound);
        
        return newSound;
    }
}