using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using SPA.Data.Models;

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

    public async Task<int> CreateGroup(string name, string description)
    {
        using (var dbConnection = Connection)
        {
            const string insert = "INSERT INTO groups(name, description) VALUES (@name, @description) RETURNING id";
            return await dbConnection.ExecuteScalarAsync<int>(insert, new { name = name, description = description });
        }
    }

    public async Task UpdateGroup(int id, string name, string description)
    {
        //TODO get existing group first and then merge the name and description

        using (var dbConnection = Connection)
        {
            const string update = "UPDATE groups SET name = @name, description = @description WHERE id = @id";
            await dbConnection.ExecuteAsync(update, new { name = name, description = description, id = id });            
        }
    }

    public async Task<IEnumerable<Group>> GetGroups()
    {
        using (var dbConnection = Connection)
        {
            const string query = "SELECT id as Id, name as Name, description as Description FROM groups";
            return await dbConnection.QueryAsync<Group>(query);
        }
    }

    public async Task<int> CreateUser(string name, string email)
    {
        using (var dbConnection = Connection)
        {
            const string insert = "INSERT INTO users(name, email) VALUES (@name, @email) RETURNING id";
            return await dbConnection.ExecuteScalarAsync<int>(insert, new { email = email, name = name });
        }
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        using (var dbConnection = Connection)
        {
            const string query = "SELECT id as Id, name as Name, email as Email FROM users";
            return await dbConnection.QueryAsync<User>(query);
        }
    }

    public async Task<User> GetUser(string email)
    {
        using (var dbConnection = Connection)
        {
            const string query = "SELECT id as Id, name as Name, email as Email FROM users WHERE email = @email";
            return await dbConnection.QueryFirstOrDefaultAsync<User>(query, new { email = email });
        }
    }

    public async Task<bool?> IsUserAdminOfGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            const string query = "SELECT is_admin FROM user_groups WHERE group_id = @group_id AND user_id = @userId";
            return await dbConnection.QueryFirstOrDefaultAsync<bool?>(query, new { groupId = groupId, userId = userId });
        }
    }

    public async Task AddUserToGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            const string insert = "INSERT INTO user_groups(group_id, user_id) VALUES (@groupId, @userId)";
            await dbConnection.ExecuteAsync(insert, new { groupId = groupId, userId = userId });
        }
    }

    public async Task RemoveUserFromGroup(int groupId, int userId)
    {
        using (var dbConnection = Connection)
        {
            const string delete = "DELETE FROM user_groups WHERE group_id = @group_id AND user_id = @userId AND is_admin = false";
            await dbConnection.ExecuteAsync(delete, new { groupId = groupId, userId = userId });
        }
    }
}
