namespace _5_WpfWithAspNetCore.Server.Services;

public sealed class GreetingService
{
    public Share.GreetingMessage CreateGreeting(string? name)
    {
        var displayName = string.IsNullOrWhiteSpace(name) ? "client" : name.Trim();

        return new Share.GreetingMessage(
            $"你好，{displayName}。此响应来自 ASP.NET Core。",
            DateTimeOffset.Now,
            Environment.MachineName);
    }
}