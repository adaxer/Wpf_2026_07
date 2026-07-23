using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

using Domain.Models;
using Domain.Models.Interfaces;

namespace DocuMan.UI.Common.ViewModels;

public partial class DocumentsViewModel : ViewModelBase
{
    private readonly IPdfDocumentService _pdfDocumentService;
    private readonly IPubSubService _pubSubService;

    public DocumentsViewModel(IPdfDocumentService pdfDocumentService, IPubSubService pubSubService)
    {
        _pdfDocumentService = pdfDocumentService;
        _pubSubService = pubSubService;
        GetDocuments();
    }

    [ObservableProperty]
    private List<PdfDocument> _pdfDocuments = [];

    private async void GetDocuments()
    {
        try
        {
            var pdfDocuments = await _pdfDocumentService.GetDocumentsAsync();
            PdfDocuments = pdfDocuments.ToList();
        }
        catch (Exception ex)
        {
            Trace.TraceError("Error retrieving documents: {0}", ex);
            _pubSubService.Publish(new StatusMessage(ex.Message));
        }
    }

    // public List<PdfDocument> PdfDocuments { get; set; } = new List<PdfDocument>();
    // public List<MarkdownDocument> MarkdownDocuments { get; set; } = new List<MarkdownDocument>();
    // public IPdfDocumentService PdfDocumentService { get; set; }
    // public IMarkdownDocumentService MarkdownDocumentService { get; set; }
}
