using System.Security.Claims;

namespace GPTOverflow.Core.Shared.Utils;

public static class ClaimExtensions
{
    public static string GetUserId(this ClaimsPrincipal claims)
    {
        return claims.Claims.Where(c => c.Type == "sub")
            .Select(c => c.Value).FirstOrDefault()!;
    }

    public static string GetUserEmail(this ClaimsPrincipal claims)
    {
        return claims.Claims.Where(c => c.Type == "email")
            .Select(c => c.Value).FirstOrDefault()!;
    }

    public static string GetValue(this ClaimsPrincipal claims, string claimType)
    {
        return claims.Claims.Where(c => c.Type == claimType)
            .Select(c => c.Value).FirstOrDefault()!;
    }
}