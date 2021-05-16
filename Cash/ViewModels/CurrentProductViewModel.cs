using Cash.Logic;
using ReactiveUI;
using System;
using System.Drawing;
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
                this.RaiseAndSetIfChanged(ref barCode, value);
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

        private Image image;
        public Image Image
        {
            get => image;
            set => this.RaiseAndSetIfChanged(ref image, value);
        }

        public Action<IShoppingItem> AddAction { private get; set; }

        public Action GainFocusOnBarCodeFieldAction { private get; set; }

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
                    Image = oneMatching.Picture;
                }
            }
            else
            {
                Clear();
            }
        }

        public void Clear(bool clearCode = false)
        {
            Name = "";
            Image = null;
            Price = 0m;
            Quantity = 0;

            if (clearCode)
            {
                BarCode = "";
                MoveFocusToBarCode();
            }
        }

        private void MoveFocusToBarCode()
        {
            if (GainFocusOnBarCodeFieldAction != null)
                GainFocusOnBarCodeFieldAction();
        }
    }
}