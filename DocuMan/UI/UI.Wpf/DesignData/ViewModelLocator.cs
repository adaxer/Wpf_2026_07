using System.IO;

using ADaxer.MvvmNav.Abstractions.Navigation;

using DocuMan.Domain.Models;
using DocuMan.Domain.Models.Interfaces;
using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

namespace DocuMan.UI.Wpf.DesignData;

public static class ViewModelLocator
{
    private static readonly ServiceProvider s_services = default!;

    // Switch DebugMode, dann funktioniert der Release-Build trotzdem, und Speicher ist gespart 

#if DEBUG
    static ViewModelLocator()
    {
        s_services = new ServiceCollection().Configure().BuildServiceProvider();
    }

    // Service Collection mit Mock-Services für DesignData, dann müssen nicht bei jeder Änderung die Aufrufe der ctors angepasst werden
    // Für Mock-Services wird NSubstitute verwendet, siehe https://nsubstitute.github.io/help/getting-started/
    // Dieses ist nicht nur für Unit-Tests geeignet, sondern auch für DesignData
    private static IServiceCollection Configure(this IServiceCollection services)
    {
        services.AddSingleton(Substitute.For<IPubSubService>());
        services.AddSingleton(Substitute.For<INavigationService>());
        
        var docs = new List<PdfDocument>
        {
            new PdfDocument("Demo", @"x:\Some.pdf"),
            new PdfDocument("Another one", @"x:\Someother.pdf")
        };
        var pdfMock = Substitute.For<IPdfDocumentService>();
        pdfMock.GetDocumentsAsync()
            .Returns(Task.FromResult<IEnumerable<PdfDocument>>(docs));
        
        services.AddSingleton(pdfMock);

        // Pdf-Demofile wird nicht in Projekt-Output kopiert, belegt nur hier Speicher
        var path = Path.Combine(Directory.GetCurrentDirectory(), "UI", "UI.Wpf", "DesignData", "PdfDemo.pdf");
        var pdfDoc = new PdfDocument("", "") { Bytes = File.ReadAllBytes(path) };

        services.AddSingleton(s => new StatusBarViewModel(s.GetRequiredService<IPubSubService>()) { StatusMessage = "StatusBar works ..." });
        services.AddSingleton(s => new PdfDocumentViewModel(s.GetRequiredService<IPubSubService>()) { Document = pdfDoc });
        services.AddTransient<ToolBarViewModel>();
        services.AddTransient<DocumentListViewModel>();
        services.AddTransient<MainViewModel>();
        return services;
    }
#endif

    public static StatusBarViewModel StatusBar => s_services.GetRequiredService<StatusBarViewModel>();

    public static DocumentListViewModel DocumentList => s_services.GetRequiredService<DocumentListViewModel>();

    public static MainViewModel Main => s_services.GetRequiredService<MainViewModel>();

    public static PdfDocumentViewModel Pdf => s_services.GetRequiredService<PdfDocumentViewModel>();
}
