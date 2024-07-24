using GuitarOnlineShop.Models;
using GuitarOnlineShop.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuitarOnlineShop.Controllers
{
    [Authorize]
    public class ConfirmEmailController : Controller
    {
        private UserManager<GuitarStoreUser> userManager;
        private IEmailSender emailSender;
        public ConfirmEmailController(UserManager<GuitarStoreUser> usrManager, IEmailSender sender)
        {
            userManager = usrManager;
            emailSender = sender;
        }

        public async Task<ViewResult> SendEmail()
        {
            var user = await userManager.GetUserAsync(User);
            string email = user.Email;
            string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string emailText = $"To confirm your email adress on Masterkusok Guitars, please, follow the link below \n" +
                $"https://{Request.Host}{Url.Action("Confirmation", "ConfirmEmail", new {id = user.Id, code = code})}";
            await emailSender.SendEmailAsync(email, "Email confirmation", emailText);
            ViewBag.PageTitle = "Confirm email";
            ViewBag.Message = "Email with instrucions for confirmation was sent to your adress.";
            return View("MessagePage");
        }

        public ViewResult Success()
        {
            ViewBag.PageTitle = "Email confirmed successfully!";
            ViewBag.Message = "Your email adress was successfully confirmed, now you can go wherever you want using navigation bar!";
            return View("MessagePage");
        }
        public ViewResult Failed()
        {
            ViewBag.PageTitle = "Email confirmation failed!";
            ViewBag.Message = "Try again later!";
            return View("MessagePage");
        }

        public async Task<IActionResult> Confirmation(string id, string code)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(code))
            {
                var user = await userManager.FindByIdAsync(id);
                if(user != null && !user.EmailConfirmed)
                {
                    var result = await userManager.ConfirmEmailAsync(user, code);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Success");
                    }
                }
            }
            return RedirectToAction("Failed");
        }
    }
}
