namespace GuitarOnlineShop.Models
{
	public interface IProductRepository
	{
		IQueryable<Product> GetProducts();
		void AddProduct(Product product);
		Product DeleteProduct(Product product);
	}
}
