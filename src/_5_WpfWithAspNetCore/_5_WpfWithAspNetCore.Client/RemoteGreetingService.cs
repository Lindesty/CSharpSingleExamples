namespace _5_WpfWithAspNetCore.Client;

public class RemoteGreetingService(IGreetingClient greetingClient)
{
    public Task<Share.GreetingMessage> HelloAsync(string? name, CancellationToken cancellationToken = default) =>
        greetingClient.HelloAsync(name, cancellationToken);
}