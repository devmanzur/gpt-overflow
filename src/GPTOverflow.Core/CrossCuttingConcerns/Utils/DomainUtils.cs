namespace GPTOverflow.Core.CrossCuttingConcerns.Utils;

public static class DomainUtils
{
    /// <summary>
    /// Creates username from email address,
    /// </summary>
    /// <example>
    /// email = manzur123@gmail.com
    /// username = @manzur123
    /// </example>
    /// <param name="email"></param>
    /// <returns></returns>
    public static string CreateUsername(string email)
    {
        return $"@{email.Split("@")[0]}";
    }
}