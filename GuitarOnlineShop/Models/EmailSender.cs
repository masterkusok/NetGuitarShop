using GuitarOnlineShop.Infrastructure.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GuitarOnlineShop.Models
{
    public class EmailSender : IEmailSender
    {
        private string senderEmail;
        private string senderPassword;

        public EmailSender(IOptions<EmailSenderConfiguration> options)
        {
            senderEmail = options.Value.Email;
            senderPassword = options.Value.Password;
        }

        public async Task SendEmailAsync(string email, string subject, string messageText)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Masterkusok's Guitars!", senderEmail));
            message.To.Add(new MailboxAddress("", email));

            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = messageText
            };
            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(senderEmail, senderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
