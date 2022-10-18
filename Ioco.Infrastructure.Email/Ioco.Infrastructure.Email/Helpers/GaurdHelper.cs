using Dawn;

namespace Ioco.Infrastructure.Email.Helpers
{
    public static class GaurdHelper
    {
        public static void CheckMailParameters(this MailRequest mailRequest)
        {
            Guard.Argument(mailRequest.FromEmail, nameof(mailRequest.FromEmail)).NotNull().NotEmpty();
            Guard.Argument(mailRequest.ToEmail, nameof(mailRequest.ToEmail)).NotNull().NotEmpty();
            Guard.Argument(mailRequest.Body, nameof(mailRequest.Body)).NotNull().NotEmpty();
        }

        public static void CheckSettingsParameters(this EmailSettings emailConfig)
        {
            Guard.Argument(emailConfig.Host, nameof(emailConfig.Host)).NotNull().NotEmpty();
            Guard.Argument(emailConfig.Port, nameof(emailConfig.Port)).NotDefault();
            Guard.Argument(emailConfig.Username, nameof(emailConfig.Username)).NotNull().NotEmpty();
            Guard.Argument(emailConfig.Password, nameof(emailConfig.Password)).NotNull().NotEmpty();
        }
    }
}
