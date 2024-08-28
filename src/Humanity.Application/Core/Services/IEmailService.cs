using Humanity.Application.Core.Models;

namespace Humanity.Application.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}