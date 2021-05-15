using Cash.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Cash.ViewModels
{
    public class ProductToPrintCodeListViewModel : ViewModelBase
    {
        public IList<PrintItemViewModel> PrintItems { get; private set; }

        private readonly IProductRepository productRepository;

        public ProductToPrintCodeListViewModel(
            IProductRepository productRepository)
        {
            this.productRepository = productRepository;

            PrintItems = new List<PrintItemViewModel>();

            PrintItems = productRepository.GetAll()
                .Select(o => new PrintItemViewModel
                {
                    Name = o.Name,
                    Code = o.BarCode,
                    Count = 0
                }).ToList();
        }
    }
}
