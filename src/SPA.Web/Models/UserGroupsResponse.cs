using System.Collections.Generic;

namespace SPA.Web.Models
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserResponse> Users { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserGroupsResponse
    {
        public UserResponse CurrentUser { get; set; }
        public List<GroupResponse> Groups { get; set; }
    }
}