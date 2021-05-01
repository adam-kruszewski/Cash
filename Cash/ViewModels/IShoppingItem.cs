namespace Cash.ViewModels
{
    public interface IShoppingItem
    {
        string Name { get; }

        int Quantity { get; }

        string BarCode { get; }

        decimal Price { get; }
    }
}