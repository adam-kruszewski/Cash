using Cash.Logic;
using ReactiveUI;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Cash.ViewModels
{
    public class PrintCodesViewModel : ViewModelBase
    {
        public string FilePath { get; set; }

        public ICommand PrintCommand { get; private set; }

        private readonly IPrintCodesService printCodeService;
        private readonly IProductRepository productRepository;
        public ProductToPrintCodeListViewModel ProductToPrintCodeList { get; private set; }

        public PrintCodesViewModel(
            IPrintCodesService printCodeService,
            IProductRepository productRepository,
            ProductToPrintCodeListViewModel productToPrintCodeList
            )
        {
            this.printCodeService = printCodeService;
            this.productRepository = productRepository;
            ProductToPrintCodeList = productToPrintCodeList;

            Initialize();
        }

        private void Initialize()
        {
            FilePath = $"BarCodes\\barcodes-{DateTime.Now.ToFileTime()}.pdf";

            PrintCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var itemsToPrint =
                    ProductToPrintCodeList
                        .PrintItems
                        .Where(o => o.Count > 0)
                        .Select(o => new ProductCodeToPrint(o.Code, o.Count, o.Name));

                var pdfBytes = printCodeService.PrintCodesToPdf(itemsToPrint);

                File.WriteAllBytes(FilePath, pdfBytes);
            });
        }
    }
}