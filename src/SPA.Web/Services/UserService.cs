using System.Threading.Tasks;
using Google.Apis.Auth;

namespace SPA.Web.Services
{
    public interface IUserService
    {
        Task<string> GetAppToken(string googleToken);
        Task AddUserToGroup(int groupId, int userId);
    }

    public class UserService : IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly IRepository _repository;

        public UserService(IJwtService jwtService, IRepository repository)
        {
            _jwtService = jwtService;
            _repository = repository;
        }

        public async Task<string> GetAppToken(string googleToken)
        {
            //validate token with Google
            var result = await GoogleJsonWebSignature.ValidateAsync(googleToken);
            
            // check whether we already have the user in our db or not
            var user = await _repository.GetUser(result.Email);
            
            //TODO: throw an exception if we somehow try to insert userId as 0
            int userId = 0;
            
            //if user not in our db then create the user
            if (user == null) userId = await _repository.CreateUser(result.Name, result.Email);
            
            //generate a token based on user info
            return _jwtService.GetToken(userId, result.Email, result.Issuer, result.Audience.ToString());
        }

        public async Task AddUserToGroup(int groupId, int userId)
            => await _repository.AddUserToGroup(groupId, userId);
    }
}