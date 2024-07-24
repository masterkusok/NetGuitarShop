namespace GuitarOnlineShop.Models
{
	public interface IBadWordsRepository
	{
		public IEnumerable<string> AllWords { get; set; }
	}
}
