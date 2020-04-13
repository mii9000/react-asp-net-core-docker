using System.ComponentModel.DataAnnotations;

namespace SPA.Web.Models
{
    public class RemoveUserFromGroupRequest
    {
        [Required]
        public int GroupId { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}