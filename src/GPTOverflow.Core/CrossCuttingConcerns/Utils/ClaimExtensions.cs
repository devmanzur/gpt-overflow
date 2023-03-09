using System.Security.Claims;

namespace GPTOverflow.Core.CrossCuttingConcerns.Utils;

public static class ClaimExtensions
{
    public static string GetValue(this ClaimsPrincipal claims, string claimType)
    {
        return claims.Claims.Where(c => c.Type == claimType)
            .Select(c => c.Value).FirstOrDefault()!;
    }
}