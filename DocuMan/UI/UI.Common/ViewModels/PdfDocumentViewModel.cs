using System.Diagnostics;

using ADaxer.MvvmNav.Abstractions.Navigation;

using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.Domain.Models;
using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class PdfDocumentViewModel : ViewModelBase, INavigationAware
{
    private readonly IPubSubService _pubSubService;

    public PdfDocumentViewModel(IPubSubService pubSubService)
    {
        _pubSubService = pubSubService;
    }

    [ObservableProperty]
    private PdfDocument? _document;

    public async Task OnNavigatedToAsync(NavigationParameters context)
    {
        try
        {
            Document = context.GetValueOrDefault<PdfDocument>("PdfDocument");
            await Document!.LoadAsync();
        }
        catch (Exception ex)
        {
            Trace.TraceError("Error loading pdf bytes: {0}", ex);
            _pubSubService.Publish(new StatusMessage(ex.Message));
        }

    }
}
