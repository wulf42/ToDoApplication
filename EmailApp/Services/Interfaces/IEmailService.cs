using EmailApp.Model;

namespace EmailApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mail body);
    }
}