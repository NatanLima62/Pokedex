using System.Net;
using System.Net.Mail;
using AutoMapper;
using Microsoft.Extensions.Options;
using Pokedex.Application.Contracts;
using Pokedex.Application.Notifications;
using Pokedex.Core.Settings;
using Pokedex.Domain.Entities;

namespace Pokedex.Application.Services;

public class EmailService : BaseService, IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly AppSettings _appSettings;
    public EmailService(IMapper mapper, INotificator notificator, IOptions<EmailSettings> emailSettings, IOptions<AppSettings> appSettings) : base(mapper, notificator)
    {
        _appSettings = appSettings.Value;
        _emailSettings = emailSettings.Value;
    }

    public void SendEmailRecoverPassword(User user)
    {
        var url = $"{_appSettings.UrlApi}?token={user.TokenRecoverPassword}&email={user.Email}";
        var body = 
            $"Olá {user.Name} segue o link para redefinir sua senha:<br>" +
            $"<a href='{url}'>Clique aqui</a><br>" +
            $"Ou use o link: {url}<br>" +
            $"Lembrando que o link tem validade de {_appSettings.ExpirationHours} horas desde o pedido de troca da senha.";
        
        var mailData = new MailData
        {
            EmailSubject = "Recuperação de senha",
            EmailBody = body,
            EmailToId = user.Email
        };

        SendEmail(mailData);
    }
    
    private void SendEmail(MailData mailData)
    {
        var toEmail = mailData.EmailToId;
        var user = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(_emailSettings.User));
        var password = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(_emailSettings.Password));

        var smtpClient = new SmtpClient(_emailSettings.Server)
        {
            Port = _emailSettings.Port,
            Credentials = new NetworkCredential(user, password),
            EnableSsl = true,
        };
        
        var mailMessage = new MailMessage(user, toEmail)
        {
            Subject = mailData.EmailSubject,
            Body = mailData.EmailBody,
            IsBodyHtml = true
        };

        try
        {
            smtpClient.Send(mailMessage);
        }
        catch (Exception)
        {
            Notificator.Handle("An error occurred while sending the email");
        }
    }
}

public class MailData
{
    public string EmailSubject { get; set; } = null!;
    public string EmailBody { get; set; } = null!;
    public string EmailToId { get; set; } = null!;
}