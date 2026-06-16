using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace _5_WpfWithAspNetCore.Client;

public static class ClientStartup
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ClientOption>(configuration.GetSection(ClientOption.SectionName));

        services
            .AddRefitClient<IGreetingClient>(new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(JsonSerializerOptions.Web)
            })
            .ConfigureHttpClient(client =>
            {
                var endpoint = configuration["ClientOption:Endpoint"]
                               ?? throw new InvalidOperationException("缺少配置项: ClientOption:Endpoint");

                client.BaseAddress = new Uri(new Uri(endpoint), "api/Greeting");
            });

        services.AddSingleton<RemoteGreetingService>();
        return services;
    }
}