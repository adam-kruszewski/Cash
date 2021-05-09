using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

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
            NotifyItemToDisplayChanged();
        }

        private void NotifyItemToDisplayChanged()
        {
            this.RaisePropertyChanged("ItemsToDisplay");
        }

        public ProductListViewModel()
        {
            itemsToDisplay = new List<ProductListItem>();
        }

        public void RemoveLast()
        {
            if (itemsToDisplay.Any())
            {
                itemsToDisplay.RemoveAt(itemsToDisplay.Count - 1);
                NotifyItemToDisplayChanged();
            }
        }

        public void RemoveAll()
        {
            itemsToDisplay.Clear();
            NotifyItemToDisplayChanged();
        }
    }
}