using System.Linq;
using ReactiveUI;

namespace Cash.ViewModels
{
    public class ShoppingActionViewModel : ViewModelBase
    {
        public ProductListViewModel ProductList { private get; set; }

        public void OnAddedItem(IShoppingItem item)
        {
            this.RaisePropertyChanged("TotalSum");
        }

        public decimal TotalSum => ProductList.ItemsToDisplay.Sum(o => o.Total);

        public void OnNew()
        {
            ProductList.RemoveAll();
        }

        public void OnRemove()
        {
            ProductList.RemoveLast();
        }
    }
}
