using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}
