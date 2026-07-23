namespace Domain.Models.Interfaces;

public interface IPdfDocumentService
{
    Task<IEnumerable<PdfDocument>> GetDocumentsAsync();
    Task<byte[]> LoadDocumentAsync(PdfDocument document);
}
