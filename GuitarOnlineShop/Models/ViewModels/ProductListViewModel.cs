using GuitarOnlineShop.Models;
namespace GuitarOnlineShop.Models.ViewModels
{
	public class ProductListViewModel
	{
		public Dictionary<string, string> Filters { get; set; }
		public IEnumerable<Product> Products { get; set; }
	}
}
