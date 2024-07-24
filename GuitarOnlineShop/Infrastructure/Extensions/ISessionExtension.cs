using Newtonsoft.Json;

namespace GuitarOnlineShop.Infrastructure.Extensions
{
	public static class ISessionExtension
	{
		public static void SaveJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetJson<T>(this ISession session, string key)
		{
			string value = session.GetString(key);
			return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); 
		}
	}
}
