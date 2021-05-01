using Cash.Logic;
using ReactiveUI;
using System;
using System.Linq;

namespace Cash.ViewModels
{
    public class CurrentProductViewModel : ViewModelBase, IShoppingItem
    {
        private readonly IProductRepository productRepository;

        private string name;

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private string barCode;

        public string BarCode
        {
            get { return barCode; }
            set
            {
                barCode = value;
                OnBarCodeChanged();
            }
        }

        private int quantity;

        public int Quantity
        {
            get => quantity;
            set => this.RaiseAndSetIfChanged(ref quantity, value);
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set => this.RaiseAndSetIfChanged(ref price, value);
        }

        public Action<IShoppingItem> AddAction { private get; set; }

        public CurrentProductViewModel(
            IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add()
        {
            if (AddAction != null)
                AddAction(this);
        }

        private void OnBarCodeChanged()
        {
            var matching = productRepository.GetByBarCode(BarCode);

            if (matching.Count() == 1)
            {
                var oneMatching = matching.Single();

                if (oneMatching != null)
                {
                    Name = oneMatching.Name;
                    Quantity = 1;
                    Price = oneMatching.Price;
                }
            }
        }
    }
}
