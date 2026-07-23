using System.Windows;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.ViewModels;
using DocuMan.UI.Wpf.Services;
using DocuMan.UI.Wpf.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DocuMan.UI.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly IHost _host = CreateHost();

    private static IHost CreateHost()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<StatusBarViewModel>();

        builder.Services.AddSingleton<IPubSubService, WpfPubSubService>();

        return builder.Build();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}