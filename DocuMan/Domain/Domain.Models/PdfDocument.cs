namespace DocuMan.Domain.Models;

public record class PdfDocument(string Name, string FilePath)
{
    public byte[]? Bytes { get; set; }

    public async Task<byte[]> LoadAsync()
    {
        Bytes = await File.ReadAllBytesAsync(FilePath);
        return Bytes;
    }
}
