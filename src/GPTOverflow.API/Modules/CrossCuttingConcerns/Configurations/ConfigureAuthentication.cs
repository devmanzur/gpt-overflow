using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Configurations;

public static class ConfigureAuthentication
{
    public static void AddAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new AuthenticationConfig();
        configuration.GetSection(nameof(AuthenticationConfig)).Bind(config);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = config.Authority;
            options.Audience = config.Audience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier,
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(config.ApiAccessScope,
                policy => policy.Requirements.Add(new HasScopeRequirement(config.ApiAccessScope,
                    config.Authority)));
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
    }

    class AuthenticationConfig
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string ApiAccessScope { get; set; }
    }

    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }

    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)?.Value
                .Split(' ');

            // Succeed if the scope array contains the required scope
            if (scopes != null && scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}