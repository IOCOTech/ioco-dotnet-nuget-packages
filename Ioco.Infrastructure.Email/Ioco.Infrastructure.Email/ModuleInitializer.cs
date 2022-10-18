using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ioco.Core.Email.Interfaces;

namespace Ioco.Infrastructure.Email;

public static class ModuleInitializer
{
    public static IServiceCollection AddEmailSupport(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.Configure<EmailSettings>(config.GetSection(nameof(EmailSettings)));
        serviceCollection.AddSingleton<ISmtpClient, SmtpClient>();
        serviceCollection.AddSingleton<IEmailService, EmailService>();

        return serviceCollection;
    }
}
