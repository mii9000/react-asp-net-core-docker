using System.Threading.Tasks;
using Google.Apis.Auth;

namespace SPA.Web.Services
{
    public interface IGroupService
    {
        Task AddUserToGroup(int groupId, int userId);
        Task RemoveUserFromGroup(int groupId, int userId);
        Task<int> CreateGroup(string name, string description);
        Task UpdateGroup(int groupId, string name, string description);
    }

    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;

        public GroupService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateGroup(string name, string description)
            => await _repository.CreateGroup(name, description);

        public async Task UpdateGroup(int groupId, string name, string description)
            => await _repository.UpdateGroup(groupId, name, description);

        public async Task AddUserToGroup(int groupId, int userId)
            => await _repository.AddUserToGroup(groupId, userId);

        public async Task RemoveUserFromGroup(int groupId, int userId)
            => await _repository.RemoveUserFromGroup(groupId, userId);
    }
}