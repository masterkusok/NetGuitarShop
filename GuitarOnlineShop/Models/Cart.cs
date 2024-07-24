namespace GuitarOnlineShop.Models
{
	public class CartLine
	{
		public int Id { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
	public class Cart
	{
		public ICollection<CartLine> CartLines { get; set; } = new List<CartLine>();

		public virtual void AddLine(Product product, int quantity)
		{
			CartLine line = CartLines.Where(x => x.Product.Id == product.Id).FirstOrDefault();
			if(line == null)
			{
				CartLines.Add(new CartLine() { Product = product, Quantity = quantity });
				return;
			}
			line.Quantity += quantity;
		}

		public virtual void RemoveLine(int id)
		{
			CartLines.Remove(CartLines.Where(x => x.Product.Id == id).FirstOrDefault());
		}

		/// <param name="quanity">Pass positive integer to increase quantity or negative to decrease quantity</param>
		public virtual void ChangeQuantity(int id, int quanity)
        {
			CartLine line = CartLines.Where(x=> x.Product.Id == id).FirstOrDefault();
			line.Quantity += quanity;
			if (line.Quantity <= 0)
				RemoveLine(line.Product.Id);
        }
		
		public decimal GetTotalPrice()
		{
			return CartLines.Sum(x => x.Product.Price * x.Quantity);
		}

		public int GetTotalQuantity()
        {
			return CartLines.Sum(x => x.Quantity);
        }

	}
}
