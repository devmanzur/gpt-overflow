using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Persistence;
using GPTOverflow.Core.StackExchange.Models;
using MediatR;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Configurations;

public static class ConfigureCrossCuttingConcerns
{
    public static void AddCrossCuttingConcerns(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // inject all handlers in that assembly
        services.AddMediatR(typeof(IEntity).Assembly);
        
        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocs("GPT Overflow", "GPT Overflow API");
        services.AddHealthChecks();
        services.AddHttpContextAccessor();
        services.AddHealthChecks();
        services.AddAuth0(configuration);
        services.AddScoped<IDomainEventsDispatcher, CrossCuttingDomainEventDispatcher>();
    }

    private static IServiceCollection AddSwaggerDocs(this IServiceCollection services, string title, string description)
    {
        services.AddSwaggerDocument(config =>
        {
            config.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = title;
                document.Info.Description = description;
                document.Info.Contact = new OpenApiContact
                {
                    Name = "Manzur Alahi",
                    Email = "devmanzur@gmail.com",
                    Url = "https://www.linkedin.com/in/devmanzur"
                };
            };
            config.AddSecurity("Bearer", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description =
                        @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below",
                });

            config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
        });
        return services;
    }
}