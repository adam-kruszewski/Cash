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

        public PrintCodesViewModel(
            IPrintCodesService printCodeService,
            IProductRepository productRepository)
        {
            this.printCodeService = printCodeService;
            this.productRepository = productRepository;

            Initialize();
        }

        private void Initialize()
        {
            FilePath = $"BarCodes\\barcodes-{DateTime.Now.ToFileTime()}.pdf";

            PrintCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var pdfBytes =
                    printCodeService.PrintCodesToPdf(
                        productRepository.GetAll().Select(o => new ProductCodeToPrint(o.BarCode, 1)));

                File.WriteAllBytes(FilePath, pdfBytes);
            });
        }
    }
}