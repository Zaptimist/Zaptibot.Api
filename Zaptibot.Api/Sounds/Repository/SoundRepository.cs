using Microsoft.Data.Sqlite;

namespace Zaptibot.Api.Sounds.Repository;

public class SoundRepository(SqliteConnection sqliteConnection) : ISoundRepository
{
    public async Task<Sound> AddAsync(Sound sound)
    {
        await sqliteConnection.OpenAsync();
        await using (SqliteCommand command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Sounds (Name, Path) VALUES (@name, @path)";
            command.Parameters.AddWithValue("@name", sound.Name);
            command.Parameters.AddWithValue("@path", sound.Path);
            await command.ExecuteNonQueryAsync();
        }

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
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Path TEXT NOT NULL
                );
            ";
            await command.ExecuteNonQueryAsync();
        }

        await sqliteConnection.CloseAsync();
    }
}