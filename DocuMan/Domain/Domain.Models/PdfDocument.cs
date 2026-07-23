namespace Domain.Models;

public record class PdfDocument(string Name, string FilePath)
{
    public byte[]? Bytes { get; private set; }

    public async Task<byte[]> LoadAsync()
    {
        Bytes = await File.ReadAllBytesAsync(FilePath);
        return Bytes;
    }
}
