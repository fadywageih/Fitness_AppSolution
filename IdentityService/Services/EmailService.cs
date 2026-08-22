using System.Net;
using System.Net.Mail;

namespace IdentityService.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");

        using var client = new SmtpClient(smtpSettings["Host"]!, int.Parse(smtpSettings["Port"]!))
        {
            EnableSsl = bool.Parse(smtpSettings["EnableSsl"]!),
            Credentials = new NetworkCredential(
                smtpSettings["UserName"],
                smtpSettings["Password"]  
            )
        };

        var mail = new MailMessage
        {
            From = new MailAddress(smtpSettings["SenderEmail"]!, smtpSettings["SenderName"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mail.To.Add(toEmail);

        await client.SendMailAsync(mail);
    }
}