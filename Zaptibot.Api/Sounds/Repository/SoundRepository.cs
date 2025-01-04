using Dapper;
using Microsoft.Data.Sqlite;
using Zaptibot.Api.Sounds.Models.Entity;

namespace Zaptibot.Api.Sounds.Repository;

public sealed class SoundRepository(SqliteConnection sqliteConnection) : ISoundRepository
{
    public async Task<Sound> AddAsync(Sound sound)
    {
        await sqliteConnection.OpenAsync();
        await sqliteConnection.ExecuteAsync(
            "INSERT INTO Sounds (Name, Path) VALUES (@name, @path)",
            new { name = sound.Name, path = sound.Path }
        );
        await sqliteConnection.CloseAsync();
        return sound;
    }

    public async Task CreateSoundsTableAsync()
    {
        await sqliteConnection.OpenAsync();
        await using (SqliteCommand command = sqliteConnection.CreateCommand())
        {
            command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Sounds (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Path TEXT NOT NULL
                );
            ";
            await command.ExecuteNonQueryAsync();
        }

        await sqliteConnection.CloseAsync();
    }

    public async Task<IEnumerable<Sound>> GetAllAsync()
    {
        await sqliteConnection.OpenAsync();
        IEnumerable<Sound> sounds = await sqliteConnection.QueryAsync<Sound>("SELECT * FROM Sounds");
        await sqliteConnection.CloseAsync();
        return sounds;
    }

    public async Task<Sound> GetAsync(int id)
    {
        await sqliteConnection.OpenAsync();
        Sound? sound = await sqliteConnection.QuerySingleOrDefaultAsync<Sound?>(
            "SELECT * FROM Sounds WHERE Id = @id",
            new { id }
        );
        await sqliteConnection.CloseAsync();
        return sound;
    }
}