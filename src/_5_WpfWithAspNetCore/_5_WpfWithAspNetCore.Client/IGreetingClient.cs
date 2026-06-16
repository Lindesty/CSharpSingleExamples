using Refit;

namespace _5_WpfWithAspNetCore.Client;

public interface IGreetingClient
{
    [Get("/hello")]
    Task<Share.GreetingMessage> HelloAsync([Query] string? name, CancellationToken cancellationToken = default);
}