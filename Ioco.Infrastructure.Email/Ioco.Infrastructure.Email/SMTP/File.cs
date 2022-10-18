namespace Ioco.Infrastructure.Email;

public class File
{
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public byte[] Content { get; set; } = null!;
}
