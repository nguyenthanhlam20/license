namespace WebApi.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
        Task SendHtmlEmailAsync(string email, string subject, string body);
    }
}