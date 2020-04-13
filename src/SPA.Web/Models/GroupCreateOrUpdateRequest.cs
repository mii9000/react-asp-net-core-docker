using System.ComponentModel.DataAnnotations;

namespace SPA.Web.Models
{
    public class GroupCreateOrUpdateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}