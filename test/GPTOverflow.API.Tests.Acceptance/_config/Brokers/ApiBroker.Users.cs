using GPTOverflow.API.Tests.Acceptance._config.Brokers.Contracts;
using GPTOverflow.API.Tests.Acceptance._config.Extensions;

namespace GPTOverflow.API.Tests.Acceptance._config.Brokers;

public partial class ApiBroker
{
    private const string UsersRelativeUrl = "api/users";
    
    public async Task<EnvelopeResponse<CreateAccountResponse>?> CreateUser(CreateAccountRequest request) =>
        await _client.SendPostRequestAsync<EnvelopeResponse<CreateAccountResponse>>(
            new RequestModel($"{UsersRelativeUrl}").AddJsonBody(request));

    public async Task<EnvelopeResponse<UserProfileResponse>?> DeleteUserById(string userId) =>
        await _client.SendDeleteRequestAsync<EnvelopeResponse<UserProfileResponse>>(new RequestModel($"{UsersRelativeUrl}/{userId}")
            .AddHeader("SecureEndpointAccess", "ki6Xerx/N0yx8M5vPCHt+w=="));
}