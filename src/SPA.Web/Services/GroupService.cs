using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPA.Web.Models;

namespace SPA.Web.Services
{
    public interface IGroupService
    {
        Task AddUserToGroup(int groupId, int userId);
        Task RemoveUserFromGroup(int groupId, int userId);
        Task<GroupResponse> CreateGroup(string name, string description, int userId, string username);
        Task UpdateGroup(int groupId, string name, string description);
        Task<UserGroupsResponse> GetAllUserGroups(int userId, string username);
    }

    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;

        public GroupService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GroupResponse> CreateGroup(string name, string description, int userId, string username)
        {
            //create a new group
            var group = await _repository.CreateGroup(name, description);
            
            //make an association with the current user and the new group
            await _repository.AddUserToGroup(group.Id, userId, true);
            
            return new GroupResponse 
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                IsAdmin = true,
                Users = new List<UserResponse>
                {
                    new UserResponse 
                    {
                        Id = userId,
                        Name = username
                    }
                }                
            };
        }

        public async Task UpdateGroup(int groupId, string name, string description)
            => await _repository.UpdateGroup(groupId, name, description);

        public async Task AddUserToGroup(int groupId, int userId)
            => await _repository.AddUserToGroup(groupId, userId);

        public async Task RemoveUserFromGroup(int groupId, int userId)
            => await _repository.RemoveUserFromGroup(groupId, userId);

        public async Task<UserGroupsResponse> GetAllUserGroups(int userId, string username)
        {
            var userGroups = (await _repository.GetAllUserGroups()).ToList();

            var currentUser = new UserResponse 
            {
                Id = userId,
                Name = username
            };

            var userGroupDict = new Dictionary<int, GroupResponse>();

            foreach (var ug in userGroups)
            {
                var key = ug.GroupId;
                if (userGroupDict.ContainsKey(key))
                {
                    userGroupDict[key].Users.Add(new UserResponse { Id = ug.UserId, Name = ug.Username });
                }
                else
                {
                    userGroupDict[key] = new GroupResponse
                    {
                        Id = ug.GroupId,
                        Name = ug.GroupName,
                        Description = ug.Description,
                        IsAdmin = ug.IsAdmin && ug.UserId == userId,
                        Users = new List<UserResponse> 
                        { 
                            new UserResponse { Id = ug.UserId, Name = ug.Username }
                        }
                    };
                }
            }

            return new UserGroupsResponse
            {
                CurrentUser = currentUser,
                Groups = userGroupDict.Select(x => x.Value).ToList()
            };
        }
    }
}