using Zaptibot.Api.Sounds.Models.Entity;
using Zaptibot.Api.Sounds.Repository;

namespace Zaptibot.Api.Sounds.Handlers;

public class GetSoundHandler(ISoundRepository soundRepository) : IGetSoundHandler
{
    public Task<IEnumerable<Sound>> GetAllAsync()
    {
        return soundRepository.GetAllAsync();
    }

    public Task<Sound> GetAsync(int id)
    {
        return soundRepository.GetAsync(id);
    }
}