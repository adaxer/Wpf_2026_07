using System.Diagnostics;

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

    public DocumentListViewModel(IPdfDocumentService pdfDocumentService, IPubSubService pubSubService)
    {
        _pdfDocumentService = pdfDocumentService;
        _pubSubService = pubSubService;
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

    [RelayCommand]
    public void ShowDocument(ItemViewModel container)
    {
        if (container.Item is PdfDocument pdfDocument)
        {
            _pubSubService.Publish(pdfDocument);
        }
    }

    // public List<PdfDocument> PdfDocuments { get; set; } = new List<PdfDocument>();
    // public List<MarkdownDocument> MarkdownDocuments { get; set; } = new List<MarkdownDocument>();
    // public IPdfDocumentService PdfDocumentService { get; set; }
    // public IMarkdownDocumentService MarkdownDocumentService { get; set; }
}
