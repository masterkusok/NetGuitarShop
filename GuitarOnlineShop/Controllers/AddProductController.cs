using GuitarOnlineShop.Models;
using GuitarOnlineShop.Models.Data;
using GuitarOnlineShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuitarOnlineShop.Controllers
{
    [Authorize(Roles = "Moderator,Administrator")]
    public class AddProductController : Controller
    {
        private readonly string[] allowedImageExtensions = { "jpg", "jpeg", "png" };

        private UserManager<GuitarStoreUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IProductRepository productRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;

        public AddProductController(UserManager<GuitarStoreUser> userMngr, RoleManager<IdentityRole> roleMngr,
            IProductRepository repository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            productRepository = repository;
            environment = hostingEnvironment;
        }

        [HttpGet]
        public ViewResult CreateProduct()
        {
            IEnumerable<Product> prods = productRepository.GetProducts();
            ViewBag.Brands = prods.Select(x => x.Brand).Distinct();
            ViewBag.Types = prods.Select(x => x.Type).Distinct();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (CanCreateSpecsDictionary(model))
                {
                    Product product = new Product()
                    {
                        Brand = model.Brand,
                        Type = model.Type,
                        Name = model.Name,
                        Series = model.Series,
                        Description = model.Description,
                        Price = model.Price,
                        Specs = CreateSpecsDictionary(model)
                    };
                    string json = JsonConvert.SerializeObject(product);
                    return RedirectToAction("UploadProductImages", "ControlPanel", new { jsonProduct = json });
                }
                ModelState.AddModelError("", "Invalid specifications dictionary");
            }
            IEnumerable<Product> prods = productRepository.GetProducts();
            ViewBag.Brands = prods.Select(x => x.Brand).Distinct();
            ViewBag.Types = prods.Select(x => x.Type).Distinct();
            return View();
        }

        private bool CanCreateSpecsDictionary(CreateProductViewModel model)
        {
            FormatSpecsLists(model);
            return model.SpecValues.Count == model.SpecKeys.Count;
        }

        private void FormatSpecsLists(CreateProductViewModel model)
        {
            model.SpecValues = model.SpecValues.Where(x => x != null).Distinct().ToList();
            model.SpecKeys = model.SpecKeys.Where(x => x != null).Distinct().ToList();
        }

        private Dictionary<string, string> CreateSpecsDictionary(CreateProductViewModel model)
        {
            return model.SpecValues.ToDictionary(x => model.SpecKeys[model.SpecValues.IndexOf(x)]);
        }

        public ViewResult UploadProductImages(string jsonProduct)
        {
            ViewData["jsonProduct"] = jsonProduct;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductImages(UploadProductImageViewModel model)
        {
            Product createdProduct = JsonConvert.DeserializeObject<Product>(model.CreatedProductJson);
            if (!UploadedFilesHaveValidExtensions(model.Files))
                ModelState.AddModelError("", "Uploaded file(s) have invalid extension");
            if (ModelState.IsValid)
            {
                createdProduct.ImgUrls = await SaveImages(model.Files);
                productRepository.AddProduct(createdProduct);
                return RedirectToAction("Index");
            }
            return View();
        }

        private bool UploadedFilesHaveValidExtensions(IEnumerable<IFormFile> files)
        {
            return files != null && files.All(file => allowedImageExtensions.Contains(file.FileName.Split(".").Last()));
        }

        private async Task<IEnumerable<string>> SaveImages(IEnumerable<IFormFile> files)
        {
            List<string> links = new List<string>();
            string path = $"/image/prods/{Guid.NewGuid()}/";
            Directory.CreateDirectory(environment.WebRootPath + path);
            foreach (var file in files)
            {
                using (Stream stream = System.IO.File.Create($"{environment.WebRootPath}/{path}{file.FileName}"))
                    await file.CopyToAsync(stream);
                links.Add(path + file.FileName);
            }
            return links;
        }
    }
}
