using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models;
using GuitarOnlineShop.Models.ViewModels;

namespace GuitarOnlineShop.Controllers
{
    public class ShopController : Controller
    {
        private IProductRepository productRepository;

        public ShopController(IProductRepository repo)
        {
            productRepository = repo;  
        }

        public IActionResult List(string Series = null, string Brand = null, string Type=null)
        {
            return View(new ProductListViewModel() {
                Products = productRepository.GetProducts()
                .Where(x => string.IsNullOrEmpty(Series) ? true : x.Series == Series)
                .Where(x => string.IsNullOrEmpty(Brand) ? true : x.Brand == Brand)
                .Where(x => string.IsNullOrEmpty(Type) ? true : x.Type == Type),
                Filters = new Dictionary<string, string>()
                {
                    {"Series", Series },
                    {"Brand", Brand },
                    {"Type", Type }
                }
            });
        }

        public ViewResult Product(int id)
		{
            return View(productRepository.GetProducts().First(prod => prod.Id == id));
		}
    }
}
