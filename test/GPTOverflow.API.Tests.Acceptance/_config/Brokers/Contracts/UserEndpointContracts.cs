namespace GPTOverflow.API.Tests.Acceptance._config.Brokers.Contracts;

public record CreateAccountRequest(string Email);
public record CreateAccountResponse(string Id, string Email, string Username, string Name, bool Suspended,string Status);
public record UserProfileResponse(string Id, string Email, string Username, string Name, bool Suspended,string Status);