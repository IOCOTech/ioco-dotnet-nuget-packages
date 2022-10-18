using Ioco.Infrastructure.Email;

namespace Ioco.Core.Email.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
    void SendEmail(MailRequest mailRequest);
}
