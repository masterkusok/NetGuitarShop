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
    public class ChangePasswordController : Controller
    {
        private IPasswordValidator<GuitarStoreUser> passwordValidator;
        private IPasswordHasher<GuitarStoreUser> passwordHasher;
        private UserManager<GuitarStoreUser> userManager;
        private IEmailSender emailSender;

        public ChangePasswordController(IPasswordValidator<GuitarStoreUser> validator, IPasswordHasher<GuitarStoreUser> hasher,
            UserManager<GuitarStoreUser> manager, IEmailSender sender)
        {
            passwordValidator = validator;
            passwordHasher = hasher;
            userManager = manager;
            emailSender = sender;
        }

        public async Task<ViewResult> EmailSent()
        {
            await SendEmailWithPassCode();
            ViewBag.PageTitle = "Email was sent to your adress";
            ViewBag.Message = $"Please, follow instructions in the mail sent to your adress";
            return View("MessagePage");
        }

        private async Task SendEmailWithPassCode()
        {
            GuitarStoreUser user = await userManager.GetUserAsync(User);
            string email = user.Email;
            string code = await userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine(code);
            Console.WriteLine(HttpUtility.UrlEncode(code));
            string emailText = $"To reset password on Masterkusok Guitars, please, follow the link below \n" +
                $"https://{Request.Host}{Url.Action(nameof(Reset), "ChangePassword", new { id = user.Id, code = HttpUtility.UrlEncode(code) })}";
            await emailSender.SendEmailAsync(user.Email, "Password reset", emailText);
        }

        public async Task<ViewResult> Reset(string id, string code)
        {
            GuitarStoreUser user = await userManager.FindByIdAsync(id);
            var success = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", HttpUtility.UrlDecode(code));

            if (success)
                return View();

            ViewBag.PageTitle = "Failed";
            ViewBag.Message = "Please, try again later";
            return View("MessagePage");
        }

        [HttpPost]
        public async Task<IActionResult> Reset(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid && PasswordsAreEqual(model))
            {
                GuitarStoreUser user = await userManager.GetUserAsync(User);
                user.PasswordHash = passwordHasher.HashPassword(user, model.NewPassword);
                
                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToSuccessPage();

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        private bool PasswordsAreEqual(ChangePasswordViewModel model)
        {
            return string.Equals(model.NewPassword, model.NewPasswordRepeat);
        }

        private IActionResult RedirectToSuccessPage()
        {
            ViewBag.PageTitle = "Success";
            ViewBag.Message = "Action was executed successfully";
            return View("MessagePage");
        }
    }
}
