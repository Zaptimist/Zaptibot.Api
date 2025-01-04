using Dapper;
using Microsoft.Data.Sqlite;
using Zaptibot.Api.Users.Models;

namespace Zaptibot.Api.Users.Repository;

public class UserRepository(SqliteConnection sqliteConnection) : IUserRepository
{
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        await sqliteConnection.OpenAsync();

        IEnumerable<User> users = await sqliteConnection.QueryAsync<User>("SELECT * FROM Users");

        await sqliteConnection.CloseAsync();

        return users;
    }

    public async Task AddUserAsync(string name)
    {
        await sqliteConnection.OpenAsync();
        await sqliteConnection.ExecuteAsync("INSERT INTO Users (Name) VALUES (@name)", new { name });
        await sqliteConnection.CloseAsync();
    }

    public async Task CreateUsersTableAsync()
    {
        await sqliteConnection.OpenAsync();
        await sqliteConnection.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL
            );
        ");
        await sqliteConnection.CloseAsync();
    }
}