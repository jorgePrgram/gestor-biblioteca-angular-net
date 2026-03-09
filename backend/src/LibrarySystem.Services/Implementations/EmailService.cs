using LibrarySystem.Services.Configuration;
using LibrarySystem.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace LibrarySystem.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly SmtpSettings smtp;
    private readonly ILogger<EmailService> logger;

    public EmailService(IOptions<SmtpSettings> options, ILogger<EmailService> logger)
    {
        smtp=options.Value;
        this.logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El correo destino no puede ser nulo ni vacío", nameof(email));
        try
        {
            var mailMessage = new MailMessage(new MailAddress(smtp.UserName, smtp.FromName),
                new MailAddress(email));

            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            using var smtpClient=new SmtpClient(smtp.Server, smtp.PortNumber)
            {
                Credentials=new NetworkCredential(smtp.UserName, smtp.Password),
                EnableSsl = smtp.EnableSsl,
            };
            await smtpClient.SendMailAsync(mailMessage);
            logger.LogInformation("Se envio correctamente el correo a {email}", email);  
        }
        catch (SmtpException ex)
        {
            logger.LogWarning("No se puede enviar e correo {message}", ex.Message);
        }
        catch(Exception ex) {
            logger.LogCritical(ex, "Error al enviar el correo a {email} {message}", email, ex.Message);
            throw;
        }
    }
}