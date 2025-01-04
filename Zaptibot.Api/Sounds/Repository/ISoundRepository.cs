using Zaptibot.Api.Sounds.Models.Entity;

namespace Zaptibot.Api.Sounds.Repository;

public interface ISoundRepository
{
    Task<Sound> AddAsync(Sound sound);
    
    /// <summary>
    /// For development purposes only
    /// </summary>
    Task CreateSoundsTableAsync();

    Task<IEnumerable<Sound>> GetAllAsync();
    Task<Sound> GetAsync(int id);
}