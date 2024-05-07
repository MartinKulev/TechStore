namespace TechStore.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false);
    }
}
