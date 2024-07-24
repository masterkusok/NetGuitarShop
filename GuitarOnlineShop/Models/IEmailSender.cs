namespace GuitarOnlineShop.Models
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string messageText);
    }
}
