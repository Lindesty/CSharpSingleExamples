namespace _5_WpfWithAspNetCore.Client;

public sealed class ClientOption
{
    public const string SectionName = nameof(ClientOption);
    public required Uri EndPoint { get; set; }
}