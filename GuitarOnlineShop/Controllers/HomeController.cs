using GuitarOnlineShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
