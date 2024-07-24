using System.ComponentModel.DataAnnotations;

namespace GuitarOnlineShop.Models.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        public string Series { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IList<string> SpecKeys { get; set; }
        [Required]
        public IList<string> SpecValues { get; set; }
        [Range(0, double.MaxValue)]
        [Required]
        public decimal Price { get; set; }
        
    }
}
