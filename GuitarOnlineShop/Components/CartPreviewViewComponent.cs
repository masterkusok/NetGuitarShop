using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models;

namespace GuitarOnlineShop.Components
{
    public class CartPreviewViewComponent : ViewComponent
    {
        private Cart cart;
        public CartPreviewViewComponent(Cart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
