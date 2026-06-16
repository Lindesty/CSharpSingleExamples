namespace _5_WpfWithAspNetCore.Share;

public record GreetingMessage(
    string Message,
    DateTimeOffset ServerTime,
    string MachineName
);