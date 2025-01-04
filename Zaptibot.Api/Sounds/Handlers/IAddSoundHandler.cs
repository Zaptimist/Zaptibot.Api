using Zaptibot.Api.Sounds.Models.Entity;

namespace Zaptibot.Api.Sounds.Handlers;

public interface IAddSoundHandler
{
    Task<Sound> HandleAsync(string name);
}