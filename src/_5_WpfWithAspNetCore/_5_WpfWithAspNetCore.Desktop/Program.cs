using System.IO;
using System.Windows;
using _5_WpfWithAspNetCore.Client;
using _5_WpfWithAspNetCore.Desktop.Services;
using _5_WpfWithAspNetCore.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _5_WpfWithAspNetCore.Desktop;

internal static class Program
{
    public static IServiceProvider Services { get; private set; } = null!;

    public static void Main(string[] args)
    {
        if (!TryLoadValidatedConfiguration(out var validatedConfiguration, out var errorMessage))
        {
            MessageBox.Show(
                errorMessage,
                "启动配置错误",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            Args = args,
            ContentRootPath = AppContext.BaseDirectory,
        });

        builder.Configuration.AddConfiguration(validatedConfiguration);

        var serverEnabled = builder.Configuration.GetValue<bool>("ServerOption:Enabled");
        builder.Services.AddLogging(logger => { logger.AddConsole(); });

        if (serverEnabled)
        {
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Server.ServerStartup).Assembly);

            var port = builder.Configuration.GetValue<ushort>("ServerOption:Port");
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
        }

        builder.Services
            .AddClientServices(builder.Configuration)
            .AddServerServices(builder.Configuration)
            .AddHostedService<WpfLifetime>()
            .AddSingleton<MainWindow>()
            .AddSingleton<MainWindowViewModel>();

        var app = builder.Build();


        if (serverEnabled)
        {
            app.MapControllers();
        }

        Services = app.Services;
        app.Run();
    }

    private static bool TryLoadValidatedConfiguration(
        out IConfigurationRoot configuration,
        out string errorMessage)
    {
        var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(appSettingsPath))
        {
            configuration = null!;
            errorMessage = $"未找到配置文件:{Environment.NewLine}{appSettingsPath}";
            return false;
        }

        try
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
        }
        catch (Exception ex)
        {
            configuration = null!;
            errorMessage = $"读取配置文件失败:{Environment.NewLine}{ex.Message}";
            return false;
        }

        var validationErrors = ValidateConfiguration(configuration);
        if (validationErrors.Count > 0)
        {
            errorMessage = "配置文件缺少或包含无效项:" + Environment.NewLine
                + string.Join(Environment.NewLine, validationErrors);
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }

    private static List<string> ValidateConfiguration(IConfiguration configuration)
    {
        var errors = new List<string>();

        if (!bool.TryParse(configuration["ServerOption:Enabled"], out var serverEnabled))
        {
            errors.Add("- ServerOption:Enabled");
            return errors;
        }

        if (serverEnabled && !ushort.TryParse(configuration["ServerOption:Port"], out _))
        {
            errors.Add("- ServerOption:Port");
        }

        var endpoint = configuration["ClientOption:Endpoint"];
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            errors.Add("- ClientOption:Endpoint");
        }
        else if (!Uri.TryCreate(endpoint, UriKind.Absolute, out _))
        {
            errors.Add("- ClientOption:Endpoint 必须是绝对 URI");
        }

        return errors;
    }
}
