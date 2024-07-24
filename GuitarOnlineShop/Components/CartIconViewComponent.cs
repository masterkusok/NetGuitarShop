using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models;

namespace GuitarOnlineShop.Components
{
    public class CartIconViewComponent : ViewComponent
    {
        private Cart cart;
        public CartIconViewComponent(Cart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
