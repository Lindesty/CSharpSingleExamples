using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _5_WpfWithAspNetCore.Desktop.Services;

public sealed class WpfLifetime : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly ILogger<WpfLifetime> _logger;

    private readonly TaskCompletionSource<Application> _applicationStarted =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    private readonly TaskCompletionSource _applicationStopped = new(TaskCreationOptions.RunContinuationsAsynchronously);

    private readonly Thread _uiThread;


    public WpfLifetime(IHostApplicationLifetime applicationLifetime, ILogger<WpfLifetime> logger)
    {
        _applicationLifetime = applicationLifetime;
        _logger = logger;

        _uiThread = new Thread(RunWpf)
        {
            Name = "WPF UI 线程",
            IsBackground = false
        };
        _uiThread.SetApartmentState(ApartmentState.STA);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _uiThread.Start();
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_uiThread.IsAlive) return;
        Application app;
        try
        {
            app = await _applicationStarted.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogInformation("get app fail, app may exited {0}",e);
            await _applicationStopped.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
            return;
        }

        if (!app.Dispatcher.HasShutdownStarted && !app.Dispatcher.HasShutdownFinished)
        {
            if (app.Dispatcher.CheckAccess())
            {
                app.Shutdown();
            }
            else
            {
                await app.Dispatcher
                    .InvokeAsync(app.Shutdown)
                    .Task
                    .WaitAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        await _applicationStopped.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    private void RunWpf()
    {
        try
        {
            var app = new App();
            _applicationStarted.TrySetResult(app);
            var mianWindow = Program.Services.GetRequiredService<MainWindow>();
            mianWindow.DataContext = Program.Services.GetRequiredService<MainWindowViewModel>();
            app.Run(mianWindow);
        }
        catch (Exception e)
        {
            _applicationStarted.TrySetException(e);
            _logger.LogError(e, "WPF UI thread terminated unexpectedly.");
        }
        finally
        {
            _applicationStopped.TrySetResult();
            _applicationLifetime.StopApplication();
        }
    }
}