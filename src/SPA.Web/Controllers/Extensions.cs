using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SPA.Web.Controllers
{
    public static class Extensions
    {
        public static int GetUserId(this HttpContext context)
        {
            var user = context.User;
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdClaim)) return 0;
            return int.Parse(userIdClaim);
        }
    }
}