using System.Net.Mail;
using System.Net;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
        {
            string MailServer = _configuration["EmailSettings:MailServer"];
            string FromEmail = _configuration["EmailSettings:FromEmail"];
            string Password = _configuration["EmailSettings:Password"];
            int Port = int.Parse(_configuration["EmailSettings:MailPort"]);

            var client = new SmtpClient(MailServer, Port)
            {
                Credentials = new NetworkCredential(FromEmail, Password),
                EnableSsl = true,
            };

            MailMessage mailMessage = new MailMessage(FromEmail, ToEmail, Subject, Body)
            {
                IsBodyHtml = IsBodyHtml
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}
