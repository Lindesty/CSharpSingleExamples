namespace _5_WpfWithAspNetCore.Server;

public sealed class ServerOption
{
    public const string SectionName = nameof(ServerOption);
    public bool Enabled { get; set; }
    public ushort Port { get; set; } = 5055;
}