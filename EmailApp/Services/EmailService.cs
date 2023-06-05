using EmailApp.Model;
using EmailApp.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace EmailApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient();
        }

        public Task SendEmailAsync(Mail mailModel)
        {
            const string smtpAddress = "127.0.0.1";
            const int smtpPort = 32771;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailModel.From));
            email.To.Add(MailboxAddress.Parse(mailModel.To));
            email.Subject = mailModel.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mailModel.Message };

            return Task.Run(async () =>
            {
                await _smtpClient.ConnectAsync(smtpAddress, smtpPort, SecureSocketOptions.None);
                await _smtpClient.SendAsync(email);
                await _smtpClient.DisconnectAsync(true);
            });
        }
    }
}