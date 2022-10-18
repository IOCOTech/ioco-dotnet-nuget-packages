namespace Ioco.Infrastructure.Email;

public class MailRequest
{
    public string FromEmail { get; set; } = null!;
    public string ToEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public List<File>? Attachments { get; set; }
}
