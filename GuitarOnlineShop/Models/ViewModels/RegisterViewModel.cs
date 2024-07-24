using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [UIHint("username")]
        public string Username { get; set; }

        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Required]
        [UIHint("password")]
        public string PasswordRepeat { get; set; }

    }
}
