using System.Collections.Generic;
using System.Linq;

namespace Cash.Logic
{
    class ProductRepository : IProductRepository
    {
        private List<Product> allProducts = GetAllProducts();

        private static List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            products.Add(new Product
            {
                BarCode = "123",
                Name = "Jabłko",
                Price = 1.23m
            });

            products.Add(
                new Product
                {
                    BarCode = "234",
                    Name = "Gruszka",
                    Price = 3.45m
                });

            products.Add(
                new Product
                {
                    BarCode = "345",
                    Name = "Śliwka",
                    Price = 0.12m
                });

            return products;
        }

        public IEnumerable<Product> GetAll()
        {
            return allProducts;
        }

        public IEnumerable<Product> GetByBarCode(string barcode)
        {
            return GetAll().Where(o => o.BarCode.ToLower().Contains(barcode));
        }
    }
}
