namespace SPA.Data.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
    }
}