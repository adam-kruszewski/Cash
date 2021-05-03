using ReactiveUI;
using System.Collections.Generic;

namespace Cash.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private List<ProductListItem> itemsToDisplay;

        public IEnumerable<ProductListItem> ItemsToDisplay =>
            itemsToDisplay.AsReadOnly();

        public void Add(IShoppingItem item)
        {
            itemsToDisplay.Add(new ProductListItem(item));
            this.RaisePropertyChanged("ItemsToDisplay");
        }

        public ProductListViewModel()
        {
            itemsToDisplay = new List<ProductListItem>();
        }

        private class FakeItem : IShoppingItem
        {
            public string Name => "Test1";

            public int Quantity => 25;

            public string BarCode => "1983";

            public decimal Price => 8.99m;
        }
    }
}