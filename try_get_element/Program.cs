using System;
using System.Linq;
using System.Xml.Linq;

namespace try_get_element
{
    class Program
    {
        static void Main(string[] args)
        {
    var doc = XDocument.Parse(source);
    var col = doc.Root.Elements("LineItems")
            .Elements("LineItem")
            .Elements("OrderLine")
            .Select(e => new
            {
                VendorPartNumber = e.Element("VendorPartNumber").Value,
                Date = e.TryGetElement("ExpectedDate", out XElement xel) ? 
                    xel.Value : 
                    String.Empty
            }).ToArray();

    foreach (var orderline in col)
    {
        Console.WriteLine(orderline.ToString());
    }
        }

    const string source =
    @"<?xml version=""1.0"" encoding=""utf-8""?>
    <root>
        <LineItems>
	        <LineItem>
		        <OrderLine>
			        <VendorPartNumber>1</VendorPartNumber>
			        <ExpectedDate>6/20/2022 4:50:34 PM</ExpectedDate>
		        </OrderLine>
		        <OrderLine>
			        <VendorPartNumber>2</VendorPartNumber>
		        </OrderLine>
	        </LineItem>
        </LineItems>
    </root>";
    }
    public static class Extensions
    {
        public static bool TryGetElement(this XElement pxel, string name, out XElement xel)
        {
            xel = pxel.Element(name);
            return xel != null;
        }
    }
}
