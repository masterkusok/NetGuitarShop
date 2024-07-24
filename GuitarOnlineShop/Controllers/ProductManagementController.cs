using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuitarOnlineShop.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class ProductManagementController : Controller
    {
    }
}
