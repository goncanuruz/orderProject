using Microsoft.Extensions.Logging;
using OrderProject.Application.Abstractions.Services;
using OrderProject.Application.DTOs;
using OrderProject.Application.Extensions;
using OrderProject.Application.Middlewares;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OrderProject.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettingDto _emailSettings;

        private readonly ILogger<SerilogMiddleware> Log;
        public EmailService(ApplicationSettings applicationSettings, ILogger<SerilogMiddleware> log)
        {
            _emailSettings = applicationSettings.EmailSettings;
            Log = log;
        }

        public async Task SendEmail(string email, string body, string subject)
        {
            try
            {
                Log.Log(LogLevel.Information, $"Mail service starting");
                MailMessage mail = new();
                mail.To.Add(email);
                mail.From = new MailAddress(_emailSettings.Username, _emailSettings.DisplayName, Encoding.UTF8);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = CreateSmtp(_emailSettings);
                await smtp.SendMailAsync(mail);

                Log.Log(LogLevel.Information, $"Mail service finished");
            }
            catch (Exception ex)
            {
                Log.LogError($"Mail Sending Error: {ex.ToJson()}");
            }
        }
        private SmtpClient CreateSmtp(EmailSettingDto emailSetting)
        {
            return new SmtpClient()
            {
                UseDefaultCredentials = emailSetting.UseDefaultCredentials,
                Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password),
                Port = emailSetting.Port,
                Host = emailSetting.Host,
                EnableSsl = emailSetting.EnableSsl,
            };
        }
    }
}
