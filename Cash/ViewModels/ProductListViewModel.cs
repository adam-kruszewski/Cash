using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Cash.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private List<IShoppingItem> items;

        public IEnumerable<IShoppingItem> Items =>
            items.AsReadOnly();

        public IEnumerable<ProductListItem> ItemsToDisplay =>
            items.Select(o => new ProductListItem(o)).ToList().AsReadOnly();

        public void Add(IShoppingItem item)
        {
            items.Add(item);
            this.RaisePropertyChanged("Items");
        }

        public ProductListViewModel()
        {
            items = new List<IShoppingItem>();
            items.Add(new FakeItem());
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