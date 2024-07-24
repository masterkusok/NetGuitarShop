using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels
{
    public class UploadProductImageViewModel
    {
        [Required]
        public IEnumerable<IFormFile> Files { get; set; }
        [Required]
        public string CreatedProductJson { get; set; }
    }
}
