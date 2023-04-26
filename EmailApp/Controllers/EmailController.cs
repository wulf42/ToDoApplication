using EmailApp.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace EmailApp.Controllers
{
    [ApiController]
    public class EmailController : ControllerBase
    {
        //private readonly IEmailService _emailService;

        //public EmailController(IEmailService emailService)
        //{
        //    _emailService = emailService;
        //}

        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult SendEmail(Mail body)
        {
            var smtpAddress = "127.0.0.1";
            var smtpPort = 32771;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(body.from));
            email.To.Add(MailboxAddress.Parse(body.to));
            email.Subject = body.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body.message };

            using var smtp = new SmtpClient();
            smtp.Connect(smtpAddress, smtpPort, SecureSocketOptions.None);
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
