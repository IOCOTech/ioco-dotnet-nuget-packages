using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Ioco.Core.Email.Interfaces;
using Ioco.Infrastructure.Email.Helpers;

namespace Ioco.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailConfig;
    private readonly ISmtpClient _smtpClient;

    public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger, ISmtpClient smtpClient)
    {
        _emailConfig = options.Value;
        _emailConfig.CheckSettingsParameters();
        _smtpClient = smtpClient;

        logger.LogInformation("Initializing email service");
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = BuildEmailMessage(mailRequest);
        await _smtpClient.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
        await _smtpClient.AuthenticateAsync(userName: _emailConfig.Username, password: _emailConfig.Password);
        await _smtpClient.SendAsync(email);
        await _smtpClient.DisconnectAsync(true);
    }

    public void SendEmail(MailRequest mailRequest)
    {
        var email = BuildEmailMessage(mailRequest);
        _smtpClient.Connect(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
        _smtpClient.Authenticate(userName: _emailConfig.Username, password: _emailConfig.Password);
        _smtpClient.Send(email);
        _smtpClient.Disconnect(true);
    }

    private MimeMessage BuildEmailMessage(MailRequest mailRequest)
    {
        mailRequest.CheckMailParameters();
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(mailRequest.FromEmail));
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        if (mailRequest.Attachments != null)
        {
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Content != null && file.Content.Length > 0)
                {
                    builder.Attachments.Add(file.FileName, file.Content, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        return email;
    }
}
