using GuitarOnlineShop.Models;
using GuitarOnlineShop.Models.Data;
using GuitarOnlineShop.Models.ViewModels.ChangeData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace GuitarOnlineShop.Controllers
{
    [Authorize]
    public class ChangeEmailController : Controller
    {
        private UserManager<GuitarStoreUser> userManager;
        private IUserValidator<GuitarStoreUser> userValidator;  
        private IEmailSender emailSender;

        public ChangeEmailController(UserManager<GuitarStoreUser> manager, IEmailSender sender,
            IUserValidator<GuitarStoreUser> validator)
        {
            userManager = manager;
            emailSender = sender;
            userValidator = validator;
        }

        public ViewResult ChangeEmail() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (InputDataIsCorrect(model))
            {
                GuitarStoreUser user = await userManager.GetUserAsync(User);
                if(await userManager.CheckPasswordAsync(user, model.Password))
                {
                    user.Email = model.NewEmail;
                    var result = await userValidator.ValidateAsync(userManager, user);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(EmailSent), new { newEmail = model.NewEmail });
                }
                ModelState.AddModelError("", "Invalid password or new email");
            }
            return View();
        }

        private bool InputDataIsCorrect(ChangeEmailViewModel model)
        {
            return !string.IsNullOrEmpty(model.NewEmail) && ModelState.IsValid;
        }

        public async Task<ViewResult> EmailSent(string newEmail)
        {
            GuitarStoreUser user = await userManager.GetUserAsync(User);
            string code = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            await SendEmailWithLink(newEmail, user, code);

            ViewBag.PageTitle = "Email was sent";
            ViewBag.Message = $"To confirm new adress, please, follow link in email sent to {newEmail}";
            return View("MessagePage");
        }

        private async Task SendEmailWithLink(string email, GuitarStoreUser user, string code)
        {
            string link = "https://" + $"{Request.Host}{Url.Action(nameof(Submit), "ChangeEmail", new { id = user.Id, code = HttpUtility.UrlEncode(code), newEmail = email })}";
            string text = $"{user.UserName}, to confirm your new email adress on Masterkusok Guitars, please, follow the link below\n"
                + link;
            await emailSender.SendEmailAsync(email, "Confirm new email", text);
        }

        public async Task<ViewResult> Submit(string id, string code, string newEmail)
        {
            GuitarStoreUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.ChangeEmailAsync(user, newEmail, HttpUtility.UrlDecode(code));
                if (result.Succeeded)
                    return Success();
            }
            return Fail();
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
