
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
    Task RemoveUserFromGroup(int groupId, int userId);
    Task<int> CreateGroup(string name, string description);
    Task<Group> GetGroup(int groupId);
    Task UpdateGroup(int id, string name);
    Task<bool?> IsUserAdminOfGroup(int groupId, int userId);
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

    public async Task UpdateGroup(int id, string name)
    {
        using (var dbConnection = Connection)
        {
            await dbConnection.ExecuteAsync("UPDATE groups SET name = @name WHERE id = @id"
            , new { name = name, id = id });            
        }
    }

    public async Task<int> CreateGroup(string name, string description)
    {
        using (var dbConnection = Connection)
        {
            //TODO test whether creating a group returns proper auto gen id or not

            var id = await dbConnection.ExecuteAsync("INSERT INTO groups(name, description) VALUES (@name, @description)"
            , new { name = name, description = description });
            
            return id;
        }
    }

    public async Task<Group> GetGroup(int id)
    {
        using (var dbConnection = Connection)
        {
            var group = await dbConnection
                .QueryFirstOrDefaultAsync<Group>("SELECT id as Id, name as Name, description as Description FROM groups WHERE id = @id"
                , new { id = id });
            
            return group;
        }
    }

    public async Task<int> CreateUser(string name, string email)
    {
        using (var dbConnection = Connection)
        {
            var userId = await dbConnection.ExecuteAsync("INSERT INTO users(name, email) VALUES (@name, @email)"
            , new { email = email, name = name });
            
            //TODO check how to get around this
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

    public async Task<bool?> IsUserAdminOfGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            var isAdmin = await dbConnection.QueryFirstOrDefaultAsync<bool?>("SELECT is_admin FROM user_groups WHERE group_id = @group_id AND user_id = @userId"
            , new { groupId = groupId, userId = userId });
            return isAdmin;
        }
    }

    public async Task AddUserToGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            await dbConnection.ExecuteAsync("INSERT INTO user_groups(group_id, user_id) VALUES (@groupId, @userId)",
                new { groupId = groupId, userId = userId });
        }
    }

    public async Task RemoveUserFromGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            await dbConnection.ExecuteAsync("DELETE FROM user_groups WHERE group_id = @group_id AND user_id = @userId AND is_admin = false",
                new { groupId = groupId, userId = userId });
        }
    }
}
