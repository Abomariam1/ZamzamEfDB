using System.Net.Mail;
using Zamzam.Application.DTOs.Email;
using Zamzam.Application.Interfaces;

namespace Zamzam.Infrastructure.Services;

public class EmailService: IEmailService
{
    public async Task SendAsync(EmailRequestDto request)
    {
        SmtpClient? emailClient = new("localhost");
        MailMessage? message = new()
        {
            From = new MailAddress(request.From),
            Subject = request.Subject,
            Body = request.Body
        };
        message.To.Add(new MailAddress(request.To));
        await emailClient.SendMailAsync(message);
    }
}
