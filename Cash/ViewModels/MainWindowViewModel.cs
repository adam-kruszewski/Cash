using System;
using System.Collections.Generic;

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
