using EduHome.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace EduHome.Services.Implements
{
    public class EmailService : IEmailService
    {
        readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendEmailAsync(string receiver, string subject, string body, bool isHTMLbody)
        {
            var server = _configuration["EmailSettings:server"];
            int.TryParse(_configuration["EmailSettings:port"], out int port);
            var email = new MimeMessage();
            var sender = _configuration["EmailSettings:sender"];
            email.From.Add(new MailboxAddress("Sender: ", sender));

            email.To.Add(MailboxAddress.Parse(receiver));

            email.Subject = subject;

            var textFormat = isHTMLbody ? TextFormat.Html : TextFormat.Text;
            email.Body = new TextPart(textFormat) { Text = body };

            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(server, port, SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(sender, _configuration["EmailSettings:password"]);

                await client.SendAsync(email);

                return "Email was sent to your email.";
            }

            finally
            {
                await client.DisconnectAsync(true);
            }
            //managed resourses - Garbage Collector terefinden idare olunur.
            //unmanaged resources - kenardan baglandigi ucun GC-a tabe deyil. using istifadesinin sebebi isi bitdikden sonra silinmesini temin etmekdir.
        }
    }
}