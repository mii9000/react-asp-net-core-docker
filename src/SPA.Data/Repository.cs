
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using SPA.Data.Models;

public interface IRepository
{
    Task<int> CreateUser(string name, string email);
    Task<User> GetUser(string email);
    Task AddUserToGroup(int groupId, int userId);
}

public class Repository : IRepository
{
    private readonly string _dbConnection;

    public Repository(string dbConnection)
    {
        _dbConnection = dbConnection;
    }

    internal IDbConnection Connection
    {
        get
        {
            return new NpgsqlConnection(_dbConnection);
        }
    }

    public async Task<int> CreateUser(string name, string email)
    {
        using (var dbConnection = Connection)
        {
            var userId = await dbConnection.ExecuteAsync("INSERT INTO users(name, email) VALUES (@name, @email)"
            , new { email = email, name = name });
            
            //due to seeding of data when spinning up the db
            //the sequence might not be on-par with app db session
            //so fetch the user again to be 100% sure about the auto generated id
            var user = await GetUser(email);

            return user.Id;
        }
    }

    public async Task<User> GetUser(string email)
    {
        using (var dbConnection = Connection)
        {
            var user = await dbConnection.QueryFirstOrDefaultAsync<User>("SELECT id as Id, name as Name, email as Email FROM users WHERE email = @email"
            , new { email = email });
            return user;
        }
    }

    public async Task AddUserToGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            var count = await dbConnection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(1) FROM users_groups WHERE group_id = @group_id AND user_id = @userId"
            , new { groupId = groupId, userId = userId });

            //user does not belong to the group
            //add the user to the group
            if (count == 0)
            {
                await dbConnection.ExecuteAsync("INSERT INTO users_groups(group_id, user_id) VALUES (@groupId, @userId)",
                    new { groupId = groupId, userId = userId });
            }
        }
    }
}
