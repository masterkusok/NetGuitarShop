using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models;

namespace GuitarOnlineShop.Components
{
	public class FilterSelectViewComponent : ViewComponent
	{
		private IProductRepository productRepository;
		public FilterSelectViewComponent(IProductRepository repo)
		{
			productRepository = repo;
		}
		public IViewComponentResult Invoke()
		{
			return View(productRepository.GetProducts());
		}
	}
}
