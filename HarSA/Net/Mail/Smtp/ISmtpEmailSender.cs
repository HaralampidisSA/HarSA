using System.Net.Mail;

namespace HarSA.Net.Mail.Smtp
{
    public interface ISmtpEmailSender : IEmailSender
    {
        SmtpClient BuildSmtpClient();
    }
}
