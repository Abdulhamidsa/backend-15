using System.Security.Claims;

namespace Api.Helpers
{
    public static class UserClaimsHelper
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? user.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException("Invalid or missing user ID claim.");

            return long.Parse(claim);
        }
    }
}
