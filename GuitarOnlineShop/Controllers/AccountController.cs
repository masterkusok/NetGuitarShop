using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GuitarOnlineShop.Models;
using GuitarOnlineShop.Models.Data;
using GuitarOnlineShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GuitarOnlineShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<GuitarStoreUser> userManager;
        private SignInManager<GuitarStoreUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IPasswordValidator<GuitarStoreUser> passwordValidator;
        private IUserValidator<GuitarStoreUser> userValidator;
        private IPasswordHasher<GuitarStoreUser> passwordHasher;

        public AccountController(UserManager<GuitarStoreUser> userMngr, SignInManager<GuitarStoreUser> signInMngr,
            IPasswordValidator<GuitarStoreUser> passValidator, IUserValidator<GuitarStoreUser> usrValidator,
            IPasswordHasher<GuitarStoreUser> passHasher, RoleManager<IdentityRole> roleMngr)
        {
            roleManager = roleMngr;
            userManager = userMngr;
            signInManager = signInMngr;
            passwordValidator = passValidator;
            userValidator = usrValidator;
            passwordHasher = passHasher;
        }


        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl, LoginViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                GuitarStoreUser user = await userManager.FindByEmailAsync(viewmodel.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, viewmodel.Password, viewmodel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View();
        }

        [AllowAnonymous]
        public ViewResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string returnUrl, RegisterViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!string.Equals(viewmodel.Password, viewmodel.PasswordRepeat))
            {
                ModelState.AddModelError("ntmtch", "Passwords do not match");
                return View();
            }

            GuitarStoreUser user = new GuitarStoreUser()
            {
                Email = viewmodel.Email,
                UserName = viewmodel.Username
            };

            var result = await userManager.CreateAsync(user, viewmodel.Password);
            await userManager.AddToRoleAsync(user, "User");

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return View();
        }

        public async Task<RedirectToActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<ViewResult> Info()
        {
            return View(await userManager.GetUserAsync(User));
        }

        public ViewResult Edit() => View(userManager.GetUserAsync(User).Result);

        [AllowAnonymous]
        public string AccessDenied() => "Access denied!";

    }
}
