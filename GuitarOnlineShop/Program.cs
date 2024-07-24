using GuitarOnlineShop.Models;
using GuitarOnlineShop.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GuitarOnlineShop.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailSenderConfiguration>(options =>
{
    var section = builder.Configuration.GetSection("Email");
    options.Email = section.GetValue<string>("Adress");
    options.Password = section.GetValue<string>("Password");
});

builder.Services.AddSession();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IBadWordsRepository, FacebookBadWordsRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDbContext<UsersDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

builder.Services.AddAuthentication();
builder.Services.AddIdentity<GuitarStoreUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<UsersDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddSession();

var app = builder.Build();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
UsersDbContext.CreateAdminAccount(app.Services, app.Configuration).Wait();
app.UseMvc(routes =>
{
    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}");
});

SeedData.EnsurePopulated(app);
app.Run();