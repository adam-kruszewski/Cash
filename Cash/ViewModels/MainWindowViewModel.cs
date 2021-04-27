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

        public MainWindowViewModel()
        {
            CurrentProduct = new CurrentProductViewModel()
            {
                Name = "Jab³ko",
                BarCode = "765",
                Quantity = 1
            };
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
