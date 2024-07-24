using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models.ViewModels.ChangeData;
using GuitarOnlineShop.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GuitarOnlineShop.Controllers
{
    [Authorize]
    public class ChangeUsernameController : Controller
    {
        IUserValidator<GuitarStoreUser> userValidator;
        UserManager<GuitarStoreUser> userManager;
        public ChangeUsernameController(IUserValidator<GuitarStoreUser> validator, UserManager<GuitarStoreUser> manager)
        {
            userValidator = validator;
            userManager = manager;
        }

        public ViewResult ChangeUsername() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel model)
        {
            if (ModelState.IsValid)
            {
                GuitarStoreUser user = await userManager.GetUserAsync(User);
                user.UserName = model.NewUsername;
                var validationResult = await userValidator.ValidateAsync(userManager, user);
                var passwordsMatch = await userManager.CheckPasswordAsync(user, model.Password);
                if (validationResult.Succeeded && passwordsMatch)
                {
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return Success();
                    else
                        return Fail();
                }
                ModelState.AddModelError("", "New username or password is invalid");
            }
            return View();
        }

        private ViewResult Success()
        {
            ViewBag.PageTitle = "Success";
            ViewBag.Message = "Operation went successfully, have a good time using our website!";
            return View("MessagePage");
        }

        private ViewResult Fail()
        {
            ViewBag.PageTitle = "Operation Failed";
            ViewBag.Message = "Please, try again later";
            return View("MessagePage");
        }
    }
}
