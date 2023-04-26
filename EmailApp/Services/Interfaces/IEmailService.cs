using EmailApp.Model;

namespace EmailApp.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Mail body);
    }
}