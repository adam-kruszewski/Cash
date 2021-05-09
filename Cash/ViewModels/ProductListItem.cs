using System.Drawing;

namespace Cash.ViewModels
{
    public class ProductListItem
    {
        public ProductListItem(IShoppingItem shoppingItem)
        {
            Name = shoppingItem.Name;
            Quantity = shoppingItem.Quantity.ToString();
            BarCode = shoppingItem.BarCode;
            Price = shoppingItem.Price;
            Total = shoppingItem.Price * shoppingItem.Quantity;
            Image = shoppingItem.Image;
        }

        public string Name { get; private set; }

        public string Quantity { get; private set; }

        public string BarCode { get; private set; }

        public decimal Price { get; private set; }

        public string PriceToDisplay => Price.ToString();

        public decimal Total { get; private set; }

        public string TotalToDisplay => Total.ToString();

        public Image Image { get; private set; }
    }
}