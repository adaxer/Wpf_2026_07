using System.Diagnostics;

using ADaxer.MvvmNav.Abstractions.Navigation;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DocuMan.Domain.Models;
using DocuMan.Domain.Models.Interfaces;
using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class DocumentListViewModel : ViewModelBase
{
    private readonly IPdfDocumentService _pdfDocumentService;
    private readonly IPubSubService _pubSubService;
    private readonly INavigationService _navigationService;

    public DocumentListViewModel(IPdfDocumentService pdfDocumentService, IPubSubService pubSubService, INavigationService navigationService)
    {
        _pdfDocumentService = pdfDocumentService;
        _pubSubService = pubSubService;
        _navigationService = navigationService;
        GetDocuments();
    }

    [ObservableProperty]
    private List<ItemViewModel> _pdfDocuments = [];

    [ObservableProperty]
    private List<NodeViewModel> _documents = [new NodeViewModel("Pdf-Dokumente", []), new NodeViewModel("MD-Dokumente", [])];

    private async void GetDocuments()
    {
        try
        {
            var pdfDocuments = await _pdfDocumentService.GetDocumentsAsync();
            PdfDocuments = pdfDocuments.Select(d=>new ItemViewModel(d.Name, d)).ToList();
            Documents?.First().Children = PdfDocuments;
        }
        catch (Exception ex)
        {
            Trace.TraceError("Error retrieving documents: {0}", ex);
            _pubSubService.Publish(new StatusMessage(ex.Message));
        }
    }

    [RelayCommand(CanExecute = nameof(CanShowDocument))]
    public async Task ShowDocument(ItemViewModel container)
    {
        if (container.Item is PdfDocument pdfDocument)
        {
            await _navigationService.NavigateAsync<PdfDocumentViewModel>(("PdfDocument", pdfDocument));
        }
    }

    // CanExecute soll schnell gehen, weil oft aufgerufen
    private bool CanShowDocument(ItemViewModel container)
    {
        return container.Item is PdfDocument;
    }

    // public List<PdfDocument> PdfDocuments { get; set; } = new List<PdfDocument>();
    // public List<MarkdownDocument> MarkdownDocuments { get; set; } = new List<MarkdownDocument>();
    // public IPdfDocumentService PdfDocumentService { get; set; }
    // public IMarkdownDocumentService MarkdownDocumentService { get; set; }
}
