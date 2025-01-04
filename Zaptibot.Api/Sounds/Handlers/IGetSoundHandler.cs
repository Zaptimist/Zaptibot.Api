using Zaptibot.Api.Sounds.Models.Entity;

namespace Zaptibot.Api.Sounds.Handlers;

public interface IGetSoundHandler
{
    Task<IEnumerable<Sound>> GetAllAsync();
    Task<Sound> GetAsync(int id);
}