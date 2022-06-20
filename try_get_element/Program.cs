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
                    PurchaseOrderNumber = doc.Root.Element("Header").Element("OrderHeader").Element("Purchase").Value,
                    VendorPartNumber = e.Element("VendorPartNumber").Value,
                    ItemStatus = e.Element("ItemStatus").Value,
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
    @"<Purchase>
        <Header>
            <OrderHeader>
                <Purchase>12345</Purchase>
            </OrderHeader>
        </Header>
        <LineItems>
            <LineItem>
                <OrderLine>
                    <VendorPartNumber>1</VendorPartNumber>
                    <UnitPrice>1073.25</UnitPrice>
                    <ExtendedLineAmount>1073.25</ExtendedLineAmount>
                    <ItemStatus>Backorder</ItemStatus>
                    <ExpectedDate>2022-05-25</ExpectedDate>
                </OrderLine>
            </LineItem>
            <LineItem>
                <OrderLine>
                    <VendorPartNumber>2</VendorPartNumber>
                    <UnitPrice>292.410000</UnitPrice>
                    <ExtendedLineAmount>584.82</ExtendedLineAmount>
                    <ItemStatus>Released</ItemStatus>
                </OrderLine>
            </LineItem>
        </LineItems>
    </Purchase>";
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
