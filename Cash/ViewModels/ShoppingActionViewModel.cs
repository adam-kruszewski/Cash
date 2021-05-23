using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Cash.ViewModels
{
    public class ShoppingActionViewModel : ViewModelBase
    {
        public ProductListViewModel ProductList { private get; set; }

        public ICommand DisplaySumCommand { get; private set; }

        public Interaction<DisplayMoneyViewModel, DisplayMoneyViewModel> ShowMoney { get; }

        public ShoppingActionViewModel()
        {
            ShowMoney = new Interaction<DisplayMoneyViewModel, DisplayMoneyViewModel>();

            DisplaySumCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var model = (DisplayMoneyViewModel)App.ServiceProvider.GetService(typeof(DisplayMoneyViewModel));

                model.SetSum(ProductList.GetSum());

                var result = await ShowMoney.Handle(model);
            });
        }
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
