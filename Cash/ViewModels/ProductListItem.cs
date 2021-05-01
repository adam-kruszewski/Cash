namespace Cash.ViewModels
{
    public class ProductListItem
    {
        public ProductListItem(IShoppingItem shoppingItem)
        {
            Name = shoppingItem.Name;
            Quantity = shoppingItem.Quantity.ToString();
            BarCode = shoppingItem.BarCode;
            Price = shoppingItem.Price.ToString();
            Total = (shoppingItem.Price * shoppingItem.Quantity).ToString();
        }

        public string Name { get; private set; }

        public string Quantity { get; private set; }

        public string BarCode { get; private set; }

        public string Price { get; private set; }

        public string Total { get; private set; }
    }
}