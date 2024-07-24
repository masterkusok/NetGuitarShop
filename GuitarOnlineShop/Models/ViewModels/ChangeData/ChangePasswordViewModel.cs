using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels.ChangeData
{
    public class ChangePasswordViewModel
    {
        [Required]
        [UIHint("password")]
        public string NewPassword { get; set; }

        [Required]
        [UIHint("password")]
        public string NewPasswordRepeat { get; set; }
    }
}
