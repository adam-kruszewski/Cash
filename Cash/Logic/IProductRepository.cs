using System.Collections.Generic;

namespace Cash.Logic
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetByBarCode(string barcode);
    }
}
