using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuitarOnlineShop.Models.Data
{
    public class UsersDbContext : IdentityDbContext<GuitarStoreUser>
    {

        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        public static async Task CreateAdminAccount(IServiceProvider provider, IConfiguration config)
        {
            using (var scope = provider.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<GuitarStoreUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<GuitarStoreUser>>();

                string username = config.GetValue<string>("AdminUser:Username");
                string email = config.GetValue<string>("AdminUser:Email");
                string password = config.GetValue<string>("AdminUser:Password");

                if (await userManager.FindByNameAsync(username) == null)
                {
                    if (!roleManager.Roles.Any())
                    {
                        await roleManager.CreateAsync(new IdentityRole("Administrator"));
                        await roleManager.CreateAsync(new IdentityRole("User"));
                        await roleManager.CreateAsync(new IdentityRole("Moderator"));
                    }
                    GuitarStoreUser user = new GuitarStoreUser()
                    {
                        UserName = username,
                        Email = email,
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var roles = roleManager.Roles.Select(x => x.Name).ToList();
                        await userManager.AddToRolesAsync(user, roles);
                    }

                }
            }
        }
    }
}
