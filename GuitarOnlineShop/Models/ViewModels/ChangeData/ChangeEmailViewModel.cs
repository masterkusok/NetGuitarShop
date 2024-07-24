using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels.ChangeData
{
    public class ChangeEmailViewModel
    {
        [Required]
        [UIHint("email")]
        public string NewEmail { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
