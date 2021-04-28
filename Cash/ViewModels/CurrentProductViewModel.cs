using Cash.Logic;
using ReactiveUI;
using System;
using System.Linq;

namespace Cash.ViewModels
{
    public class CurrentProductViewModel : ViewModelBase
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

        public CurrentProductViewModel(
            IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add()
        {
            Console.WriteLine($"Adding product {Name}");
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
                }
            }
        }
    }
}
