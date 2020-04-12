using System.Threading.Tasks;
using Google.Apis.Auth;

namespace SPA.Web.Services
{
    public interface IGroupService
    {
        Task<bool> AddUserToGroup(int groupId, int userId);
        Task<bool> RemoveUserFromGroup(int groupId, int userId);
        Task CreateGroup(string name, string description);
        Task UpdateGroup(int groupId, string name);
    }

    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;

        public GroupService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddUserToGroup(int groupId, int userId)
        {
            var isUserAdminOfGroup = await _repository.IsUserAdminOfGroup(groupId, userId);
            //user can be or not be the admin of the group
            //but having a value returns means user is already part of the group
            if(isUserAdminOfGroup.HasValue) return false;
            await _repository.AddUserToGroup(groupId, userId);
            return true;
        }

        public async Task<bool> RemoveUserFromGroup(int groupId, int userId)
        {
            var isUserAdminOfGroup = (await _repository.IsUserAdminOfGroup(groupId, userId)).GetValueOrDefault();
            //user does not belong to group or not admin
            if(!isUserAdminOfGroup) return false;
            await _repository.RemoveUserFromGroup(groupId, userId);
            return true;         
        }

        public async Task CreateGroup(string name, string description)
            => await _repository.CreateGroup(name, description);

        public async Task UpdateGroup(int groupId, string name)
            => await _repository.UpdateGroup(groupId, name);
    }
}