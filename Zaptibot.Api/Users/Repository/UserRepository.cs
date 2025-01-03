using Microsoft.Data.Sqlite;
using Zaptibot.Api.Users.Models;

namespace Zaptibot.Api.Users.Repository;

public class UserRepository(SqliteConnection sqliteConnection) : IUserRepository
{
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        await sqliteConnection.OpenAsync();

        SqliteCommand command = sqliteConnection.CreateCommand();
        command.CommandText = "SELECT * FROM Users";
        List<User> users = new List<User>();

        await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                users.Add(
                    new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    }
                );
            }
        }

        await sqliteConnection.CloseAsync();

        return users;
    }

    public async Task AddUserAsync(string name)
    {
        await sqliteConnection.OpenAsync();
        await using (SqliteCommand command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Users (Name) VALUES (@name)";
            command.Parameters.AddWithValue("@name", name);
            await command.ExecuteNonQueryAsync();
        }

        await sqliteConnection.CloseAsync();
    }

    public async Task CreateUsersTableAsync()
    {
        await sqliteConnection.OpenAsync();
        await using (SqliteCommand command = sqliteConnection.CreateCommand())
        {
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL
                );
            ";
            await command.ExecuteNonQueryAsync();
        }
        await sqliteConnection.CloseAsync();
    }
}