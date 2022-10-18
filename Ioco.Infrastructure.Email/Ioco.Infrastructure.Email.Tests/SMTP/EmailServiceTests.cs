using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ioco.Infrastructure.Email.Tests.SMTP
{
    public class EmailServiceTests
    {
        private Mock<ISmtpClient> _smtpClientMock;
        private Mock<ILogger<EmailService>> _loggerMock;
        private IOptions<EmailSettings> _emailSettingsOptions;
        private MailRequest _mailRequest;

        private EmailService _emailService;

        public EmailServiceTests()
        {
            _smtpClientMock = new Mock<ISmtpClient>();
            _loggerMock = new Mock<ILogger<EmailService>>();
            _emailSettingsOptions = Options.Create(new EmailSettings {
                Host = "localhost",
                Port = 1212,
                Username = "username",
                Password = "pass"
            });
            _emailService = new EmailService(
                _emailSettingsOptions,
                _loggerMock.Object,
                _smtpClientMock.Object);

            _mailRequest = new MailRequest
            {
                Body = "Test",
                Subject = "subject test",
                FromEmail = "noreply@xxx.com",
                ToEmail = "xx@xxx.com",
                Attachments = new List<File>
                    {
                        new File
                        {
                            FileName = "test.txt",
                            Content = Encoding.ASCII.GetBytes("testing files"),
                            ContentType = "text/plain"
                        }
                    }
            };
        }

        [Fact]
        public async Task Can_SendEmailAsync()
        {
            await _emailService.SendEmailAsync(_mailRequest);
        }

        [Fact]
        public async Task Can_Throw_Exception_When_Bad_Request_For_SendEmailAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _emailService.SendEmailAsync(new MailRequest()));
        }

        [Fact]
        public void Can_SendEmail()
        {
            _emailService.SendEmail(_mailRequest);
        }

        [Fact]
        public void Can_Throw_Exception_When_Bad_Request_For_SendEmail()
        {
            Assert.Throws<ArgumentNullException>(() => _emailService.SendEmail(new MailRequest()));
        }
    }
}
