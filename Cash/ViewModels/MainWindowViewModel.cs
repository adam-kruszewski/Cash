using Cash.Logic;
using Cash.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cash.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public string Greeting2 => "Hello world";

        public List<ListItem> MyItems => GetItems();

        public CurrentProductViewModel CurrentProduct { get; private set; }

        public ProductListViewModel ProductList { get; private set; }

        public ShoppingActionViewModel ShoppingActions { get; private set; }

        private readonly IProductRepository productRepository;

        public ICommand PrintCodesCommand { get; }

        public Interaction<PrintCodesViewModel, PrintCodesViewModel> ShowDialog { get; }

        public MainWindowViewModel(
            IProductRepository productRepository,
            CurrentProductViewModel currentProduct,
            ProductListViewModel productList,
            ShoppingActionViewModel shoppingActions)
        {
            this.productRepository = productRepository;

            CurrentProduct = currentProduct;
            ProductList = productList;
            ShoppingActions = shoppingActions;

            ShoppingActions.ProductList = productList;

            CurrentProduct.AddAction = item => OnAddCurrentItem(item);

            ShowDialog = new Interaction<PrintCodesViewModel, PrintCodesViewModel>();

            PrintCodesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var model = (PrintCodesViewModel) App.ServiceProvider.GetService(typeof(PrintCodesViewModel));

                var result = await ShowDialog.Handle(model);
            });
        }

        private void OnAddCurrentItem(IShoppingItem item)
        {
            ProductList.Add(item);
            CurrentProduct.Clear(clearCode: true);
            ShoppingActions.OnAddedItem(item);
        }

        private List<ListItem> GetItems()
        {
            var list = new List<ListItem>();
            list.Add(new ListItem());
            list.Add(new ListItem());

            return list;
        }

        public void OnClickOpen()
        {
            var a = 1;
        }

        public void OnPrintCodes()
        {
            Console.WriteLine("On print codes");
        }
    }

    public class ListItem
    {
        public ListItem()
        {
            Text = DateTime.Now.ToString();
        }

        public string Text { get; set; }
    }
}
