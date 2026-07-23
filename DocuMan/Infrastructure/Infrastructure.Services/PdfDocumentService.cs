using Domain.Models;
using Domain.Models.Interfaces;

namespace Infrastructure.Services;

public class PdfDocumentService : IPdfDocumentService
{
    const string DocumentsFolder = ".\\Pdf";

    public Task<IEnumerable<PdfDocument>> GetDocumentsAsync()
    {
        var documents = Directory.GetFiles(DocumentsFolder, "*.pdf")
            .Select(filePath => new PdfDocument(Path.GetFileNameWithoutExtension(filePath), filePath));
        return Task.FromResult(documents);
    }

    public Task<byte[]> LoadDocumentAsync(PdfDocument document)
    {
        return document.LoadAsync();
    }
}
