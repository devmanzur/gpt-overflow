using FluentAssertions;
using GPTOverflow.API.Tests.Acceptance._config.Brokers.Contracts;
using GPTOverflow.Core.StackExchange.Brokers.Persistence;
using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GPTOverflow.API.Tests.Acceptance.UserManagement.Controllers;

public partial class UsersControllerTests
{
    [Fact]
    public async Task ShouldCreateUserAccountSuccessfully()
    {
        //given
        var email = _faker.Person.Email;
        var request = new CreateAccountRequest(email);

        //when
        var createResponse = await _apiBroker.CreateUser(request);
        var response = createResponse!.Body;
        
        //then
        response.Should().NotBeNull();
        response.Id.Should().NotBeNull();
        response.Email.Should().Be(email);
        response.Username.Should().Be(CreateUsername(email));
        response.Suspended.Should().BeFalse();
        response.Status.Should().Be("Active");
        
        var scope = _api.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        var userManagementDbContext = scope?.ServiceProvider.GetService<UserManagementDbContext>();
        var stackExchangeDbContext = scope?.ServiceProvider.GetService<StackExchangeDbContext>();

        var savedUser = 
            await userManagementDbContext!.Users.FirstOrDefaultAsync(x => x.Username == response.Username);
        savedUser.Should().NotBeNull();
        savedUser!.Username.Should().NotBeNull();
        savedUser.EmailAddress.Should().Be(response.Email);

        var savedAccount =
            await stackExchangeDbContext!.Accounts.FirstOrDefaultAsync(x => x.Username == response.Username);
        savedAccount.Should().NotBeNull();
        savedAccount!.Username.Should().NotBeNull();
    }
}