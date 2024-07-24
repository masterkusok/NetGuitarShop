using GuitarOnlineShop.Models.Data;
using GuitarOnlineShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuitarOnlineShop.Controllers
{
    [Authorize(Roles = "Administrator,Moderator")]
    public class UserManagementController : Controller
    {
        private UserManager<GuitarStoreUser> userManager;

        public async Task<ViewResult> UserManagement()
        {
            var moderators = await userManager.GetUsersInRoleAsync("Moderator");
            var users = userManager.Users.ToList().Except(moderators);
            return View((moderators.Except(await userManager.GetUsersInRoleAsync("Administrator")), users));
        }

        public async Task<RedirectToActionResult> SwitchUserRole(string id, string role)
        {
            var user = await userManager.FindByIdAsync(id);
            IdentityResult result;
            if (role == "Moderator")
                result = await userManager.AddToRoleAsync(user, role);
            else
                result = await userManager.RemoveFromRoleAsync(user, "Moderator");
            TempData["Message"] = result.Succeeded ? $"{id} role was switched to {role} successfully" :
                $"Error while switching the role^ \n {result.Errors.First().Description}";
            return RedirectToAction("UserManagement");
        }

        public async Task<RedirectToActionResult> DeleteUser(string id)
        {
            GuitarStoreUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            string message = (result.Succeeded ? $"User deleted successfully" : $"Error while deleting user: " +
                $"\n {result.Errors.First().Description}");
            TempData["message"] = message;
            return RedirectToAction("UserManagement");
        }
    }
}
