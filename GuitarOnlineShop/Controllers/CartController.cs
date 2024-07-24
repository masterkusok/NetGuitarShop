using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace GuitarOnlineShop.Controllers
{
	public class CartController : Controller
	{
		private Cart cart;
		private IProductRepository productRepository;
		public CartController(Cart shoppingCart, IProductRepository repo)
		{
			cart = shoppingCart;
			productRepository = repo;
		}

		public RedirectResult AddItem(string returnUrl, int id)
		{
			cart.AddLine(productRepository.GetProducts().Where(x => x.Id == id).FirstOrDefault(), 1);
			return Redirect(returnUrl);
		}

		public RedirectToActionResult RemoveLine(int id)
        {
			cart.RemoveLine(id);
			return RedirectToAction("Details");
        }

		public RedirectToActionResult ChangeQuantity(int id, int quantity)
		{
			cart.ChangeQuantity(id, quantity);
			return RedirectToAction("details");
		}

		[Authorize]
		public ViewResult Details()
        {
			return View(cart);
        }
	}
}
