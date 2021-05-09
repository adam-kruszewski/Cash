using Cash.Logic.XmlRepositories;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Cash.Logic
{
    class ProductRepository : IProductRepository
    {
        private List<Product> allProducts;

        private List<Product> InitProducts()
        {
            if (allProducts == null)
            {
                allProducts = new List<Product>();

                var serializer = new XmlSerializer(typeof(XmlProductRepository));

                using (var fs = new FileStream("products.xml", FileMode.Open))
                {
                    var xmlProducts = (XmlProductRepository) serializer.Deserialize(fs);

                    foreach (var xmlProduct in xmlProducts.Procucts)
                    {
                        allProducts.Add(new Product
                        {
                            BarCode = xmlProduct.BarCode,
                            Name = xmlProduct.Name,
                            Price = xmlProduct.Price,
                            Picture = Image.FromFile(xmlProduct.ImageFileName)
                        });
                    }
                }

                
            }            //var products = new List<Product>();

            return allProducts;

            //products.Add(new Product
            //{
            //    BarCode = "123",
            //    Name = "Jabłko",
            //    Price = 1.23m
            //});

            //products.Add(
            //    new Product
            //    {
            //        BarCode = "234",
            //        Name = "Gruszka",
            //        Price = 3.45m
            //    });

            //products.Add(
            //    new Product
            //    {
            //        BarCode = "345",
            //        Name = "Śliwka",
            //        Price = 0.12m
            //    });

            //var pr = new XmlProductRepository();

            //foreach (var p in products)
            //{
            //    pr.Procucts.Add(new XmlProduct
            //    {
            //        BarCode = p.BarCode,
            //        Name = p.Name,
            //        Price = p.Price,
            //        ImageFileName = "A.JPG"
            //    });
            //}

            //var serializer = new XmlSerializer(typeof(XmlProductRepository));

            //using (var fs = new FileStream("products.xml", FileMode.OpenOrCreate))
            //{
            //    serializer.Serialize(fs, pr);
            //}

            //return products;
        }

        public IEnumerable<Product> GetAll()
        {
            InitProducts();
            return allProducts;
        }

        public IEnumerable<Product> GetByBarCode(string barcode)
        {
            InitProducts();
            return GetAll().Where(o => o.BarCode.ToLower().Contains(barcode));
        }
    }
}
