using _5_WpfWithAspNetCore.Server.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _5_WpfWithAspNetCore.Server;

public static class ServerStartup
{
    public static IServiceCollection AddServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServerOption>(configuration.GetSection(ServerOption.SectionName));
        services.AddSingleton<GreetingService>();
        return services;
    }

}