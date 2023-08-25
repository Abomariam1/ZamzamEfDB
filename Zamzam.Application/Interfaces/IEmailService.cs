using Zamzam.Application.DTOs.Email;

namespace Zamzam.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}
