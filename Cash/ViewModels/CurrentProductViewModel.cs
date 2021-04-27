using System;

namespace Cash.ViewModels
{
    public class CurrentProductViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public string BarCode { get; set; }

        public int Quantity { get; set; }

        public void Add()
        {
            Console.WriteLine($"Adding product {Name}");
        }
    }
}
