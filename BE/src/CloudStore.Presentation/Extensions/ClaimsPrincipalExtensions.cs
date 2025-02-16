using System.Security.Claims;

namespace CloudStore.Presentation.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserIdentityId(this ClaimsPrincipal user)
    {
        return user.Claims
            .SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)
            ?.Value!;
    }
}