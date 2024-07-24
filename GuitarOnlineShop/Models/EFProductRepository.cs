using GuitarOnlineShop.Models.Data;

namespace GuitarOnlineShop.Models
{
	public class EFProductRepository:IProductRepository
	{
		private ApplicationDbContext context;
		public EFProductRepository(ApplicationDbContext dbContext)
		{
			context = dbContext;
		}
		public void AddProduct(Product product)
		{
			context.Products.Add(product);
			context.SaveChanges();
		}

		public Product DeleteProduct(Product product)
		{
			Product prod = context.Products.FirstOrDefault(p => p.Id == product.Id);
			context.Products.Remove(prod);
			context.SaveChanges();
			return prod;
		}

		public IQueryable<Product> GetProducts()
		{
			return context.Products;
		}
	}
}
