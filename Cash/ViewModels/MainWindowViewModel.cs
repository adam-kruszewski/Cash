using System;
using System.Collections.Generic;
using System.Text;

namespace Cash.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public string Greeting2 => "Hello world";

        public List<ListItem> MyItems => GetItems();

        private List<ListItem> GetItems()
        {
            var list = new List<ListItem>();
            list.Add(new ListItem());

            return list;
        }
    }

    public class ListItem
    {

    }
}
