namespace Zaptibot.Api.Sounds.Repository;

public interface ISoundRepository
{
    Task<Sound> AddAsync(Sound sound);
    
    /// <summary>
    /// For development purposes only
    /// </summary>
    Task CreateSoundsTableAsync();
}