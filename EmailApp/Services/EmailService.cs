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

        public void SendEmail(Mail mailModel)
        {
            string smtpAddress = "127.0.0.1";
            int smtpPort = 32771;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailModel.from));
            email.To.Add(MailboxAddress.Parse(mailModel.to));
            email.Subject = mailModel.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mailModel.message };

            _smtpClient.Connect(smtpAddress, smtpPort, SecureSocketOptions.None);
            _smtpClient.Send(email);
            _smtpClient.Disconnect(true);
        }
    }
}