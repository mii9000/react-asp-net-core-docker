using System.Collections.Generic;
using System.Threading.Tasks;
using SPA.Data.Models;

public interface IRepository
{
    //users
    Task<int> CreateUser(string name, string email);
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(string email);
    Task<User> GetUser(int id);


    //groups
    Task<Group> CreateGroup(string name, string description);
    Task<IEnumerable<Group>> GetGroups();
    Task UpdateGroup(int id, string name, string description);

    //user_groups
    Task<bool?> IsUserAdminOfGroup(int groupId, int userId);
    Task AddUserToGroup(int groupId, int userId, bool isAdmin = false);
    Task RemoveUserFromGroup(int groupId, int userId);
    Task<IEnumerable<UserGroup>> GetAllUserGroups();
}
