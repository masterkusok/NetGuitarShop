using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels.ChangeData
{
    public class ChangeUsernameViewModel
    {
        [Required]
        [UIHint("text")]
        public string NewUsername { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
