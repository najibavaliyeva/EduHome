namespace EduHome.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string receiver, string subject, string body, bool isHTMLbody); 
    }
}
