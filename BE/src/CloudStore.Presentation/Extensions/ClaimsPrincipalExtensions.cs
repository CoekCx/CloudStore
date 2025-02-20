using System.Security.Claims;
using CloudStore.Domain.Abstractions.Exceptions;
using CloudStore.Domain.Exceptions;

namespace CloudStore.Presentation.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserIdentityId(this ClaimsPrincipal user)
    {
        var stringId = user.Claims
            .SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)
            ?.Value! ?? throw new UnauthorizedException();

        if (!Guid.TryParse(stringId, out var result))
            throw new UnauthorizedException();

        return result;
    }
}