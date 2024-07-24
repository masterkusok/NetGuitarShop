using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GuitarOnlineShop.Models
{
	[Serializable]
	public class Product
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Series { get; set; }
		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Type { get; set; }
		public string XmlImgUrls
        {
            get
            {
				XElement rootElement = new XElement("Images", ImgUrls.Select(url => new XElement("Img", new XAttribute("Url", url))));
				return rootElement.ToString();
            }
            set
            {
				XElement rootElement = XElement.Parse(value);
				ImgUrls = rootElement.Descendants("Img").Select(x => (string)x.Attribute("Url")).ToArray();
            }
        }
		public string XmlSpecs { 
			get
            {
				XElement rootElem = new XElement(
					"Specs", Specs.Select(spec => new XElement("Spec", 
					new XAttribute("Key", spec.Key), new XAttribute("Value", spec.Value))));
				return rootElem.ToString();
            }
			set
            {
				XElement rootElem = XElement.Parse(value);
				Specs = rootElem.Descendants("Spec").ToDictionary(x => (string)x.Attribute("Key"), x => (string)x.Attribute("Value"));
            }
		}
		[NotMapped]
		public IEnumerable<string> ImgUrls { get; set; } = new List<string>();
		[NotMapped]
		public Dictionary<string, string> Specs { get; set; } = new Dictionary<string, string>();
	}
}
