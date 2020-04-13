using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SPA.Web.Controllers
{
    public static class Extensions
    {
        public static (int UserId, string Username) GetUserClaims(this HttpContext context)
        {
            var user = context.User;
            var idClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = string.IsNullOrWhiteSpace(idClaim) ? 0 : int.Parse(idClaim);
            var nameClaim = user.FindFirstValue(ClaimTypes.Name);
            return (id, nameClaim);
        }
    }
}