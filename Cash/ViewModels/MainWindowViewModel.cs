using Cash.Logic;
using System;
using System.Collections.Generic;

namespace Cash.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public string Greeting2 => "Hello world";

        public List<ListItem> MyItems => GetItems();

        public CurrentProductViewModel CurrentProduct { get; set; }

        private readonly IProductRepository productRepository;

        public MainWindowViewModel(
            IProductRepository productRepository,
            CurrentProductViewModel currentProduct)
        {
            this.productRepository = productRepository;

            CurrentProduct = currentProduct;
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
