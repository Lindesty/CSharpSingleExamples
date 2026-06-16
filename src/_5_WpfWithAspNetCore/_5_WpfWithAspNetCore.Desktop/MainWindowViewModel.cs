using _5_WpfWithAspNetCore.Client;
using _5_WpfWithAspNetCore.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;

namespace _5_WpfWithAspNetCore.Desktop;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly RemoteGreetingService _remoteGreetingService;
    private readonly IOptions<ServerOption> _serverOptions;
    private readonly IOptions<ClientOption> _clientOptions;

    public MainWindowViewModel(
        RemoteGreetingService remoteGreetingService,
        IOptions<ServerOption> serverOptions,
        IOptions<ClientOption> clientOptions)
    {
        _remoteGreetingService = remoteGreetingService;
        _serverOptions = serverOptions;
        _clientOptions = clientOptions;

        ModeSummary = $"服务端已启用: {serverOptions.Value.Enabled}; " +
                      $"服务端端口: {serverOptions.Value.Port}; " +
                      $"客户端端点: {clientOptions.Value.EndPoint}";
    }

    public string ModeSummary { get; private set => SetProperty(ref field, value); }

    public string Name { get; set => SetProperty(ref field, value); } = Environment.UserName;

    public string Result { get; set => SetProperty(ref field, value); } =
        "点击「Call API」按钮。如果 Server:Enabled 为 false，请先启动另一份启用了 Server:Enabled=true 的实例。";

    [RelayCommand]
    private async Task CallServerAsync()
    {
        try
        {
            Result = "正在调用服务器...";
            var response = await _remoteGreetingService.HelloAsync(Name);
            Result = $"{response.Message}{Environment.NewLine}" +
                     $"服务器时间: {response.ServerTime:O}{Environment.NewLine}" +
                     $"服务器机器名: {response.MachineName}";
        }
        catch (Exception ex)
        {
            Result = $"请求失败: {ex.Message}";
        }
    }
}