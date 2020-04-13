using System.ComponentModel.DataAnnotations;

namespace SPA.Web.Models
{
    public class GoogleLogin
    {
        [Required]
        public string Token { get; set; }
    }
}