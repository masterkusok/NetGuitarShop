namespace GuitarOnlineShop.Models
{
	public class FacebookBadWordsRepository : IBadWordsRepository
	{
		public FacebookBadWordsRepository()
		{
			using(StreamReader reader = new StreamReader("bwl.txt"))
			{
				AllWords = reader.ReadToEnd().Split(",");
			}
		}
		public IEnumerable<string> AllWords { get; set; }
	}
}
