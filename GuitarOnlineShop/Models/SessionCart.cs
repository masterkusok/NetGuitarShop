using GuitarOnlineShop.Infrastructure.Extensions;
using System.Text.Json.Serialization;

namespace GuitarOnlineShop.Models
{
	public class SessionCart : Cart
	{
		[JsonIgnore]
		private static ISession session;

		public static Cart GetCart(IServiceProvider provider)
		{
			session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			SessionCart cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();
			return cart;
		}

		public override void AddLine(Product product, int quantity)
		{
			base.AddLine(product, quantity);
			session.SaveJson("cart", this);
		}

		public override void RemoveLine(int id)
		{
			base.RemoveLine(id);
			session.SaveJson("cart", this);
		}

        public override void ChangeQuantity(int id, int quantity)
        {
            base.ChangeQuantity(id, quantity);
			session.SaveJson("cart", this);
        }
    }
}
