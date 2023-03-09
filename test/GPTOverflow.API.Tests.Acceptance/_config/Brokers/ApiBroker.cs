namespace GPTOverflow.API.Tests.Acceptance._config.Brokers;

public partial class ApiBroker
{
    private readonly HttpClient _client;

    public ApiBroker(HttpClient client)
    {
        _client = client;
    }
}