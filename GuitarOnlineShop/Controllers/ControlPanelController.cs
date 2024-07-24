using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuitarOnlineShop.Controllers
{
    [Authorize(Roles = "Moderator,Administrator")]
    public class ControlPanelController : Controller
    {
        public ViewResult Index() => View(User.IsInRole("Administrator"));
    }
}
