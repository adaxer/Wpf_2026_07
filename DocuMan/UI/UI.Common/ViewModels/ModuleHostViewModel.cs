using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using DocuMan.Domain.Models;
using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class ModuleHostViewModel : ViewModelBase, IRecipient<PdfDocument>
{
    public ModuleHostViewModel(IPubSubService pubSubService)
    {
        pubSubService.Subscribe<PdfDocument>(this);
        _pubSubService = pubSubService;
    }

    [ObservableProperty]
    private object? _content;
    private readonly IPubSubService _pubSubService;

    public async void Receive(PdfDocument document)
    {
        try
        {
            await document.LoadAsync();
            Content = document;
        }
        catch (Exception ex)
        {
            Trace.TraceError("Error loading pdf bytes: {0}", ex);
            _pubSubService.Publish(new StatusMessage(ex.Message));
        }
    }
}
