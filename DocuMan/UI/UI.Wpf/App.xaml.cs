using System.Windows;

using ADaxer.MvvmNav.Abstractions.Navigation;
using ADaxer.MvvmNav.Wpf.Hosting;

using DocuMan.Domain.Models.Interfaces;
using DocuMan.Infrastructure.Services;
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
    private static WpfNavigationHost? _host;

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<StatusBarViewModel>();
        services.AddSingleton<ToolBarViewModel>();
        services.AddSingleton<DocumentListViewModel>();
        services.AddTransient<PdfDocumentViewModel>();
        services.AddTransient<LoginViewModel>();

        services.AddTransient<IPdfDocumentService,PdfDocumentService>();
        services.AddSingleton<IPubSubService, WpfPubSubService>();  // Besser Singleton, weil Registrierung von EventHandlern usw.
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        _host = WpfNavigationHostBuilder
            .Default()
            .WithShell<MainWindow, MainViewModel>()
            .WithServices(ConfigureServices)
            .WithDialogMode(DialogMode.Window)
            .Build();

        await _host.StartAsync();
        base.OnStartup(e);
    }

}