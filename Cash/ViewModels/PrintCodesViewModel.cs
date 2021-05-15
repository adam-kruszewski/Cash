using Cash.Logic;
using ReactiveUI;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Cash.ViewModels
{
    public class PrintCodesViewModel : ViewModelBase
    {
        public string FilePath { get; set; }

        public ICommand PrintCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public Action CloseAction { private get; set; }

        public Image CloseImage { get; private set; }

        private readonly IPrintCodesService printCodeService;
        private readonly IProductRepository productRepository;
        public ProductToPrintCodeListViewModel ProductToPrintCodeList { get; private set; }

        public PrintCodesViewModel(
            IPrintCodesService printCodeService,
            IProductRepository productRepository,
            ProductToPrintCodeListViewModel productToPrintCodeList)
        {
            this.printCodeService = printCodeService;
            this.productRepository = productRepository;
            ProductToPrintCodeList = productToPrintCodeList;

            Initialize();
        }

        private void Initialize()
        {
            var resourceName =
            GetType().Assembly.GetManifestResourceNames().Single(o => o.EndsWith("jablko.jpg"));

            //using (var stream = GetType().Assembly.GetManifestResourceStream(resourceName))
            using (var stream = new FileStream("c:\\adam\\projekty\\git\\Cash\\Cash\\Icons\\jablko.jpg", FileMode.Open))
            {
                CloseImage = Image.FromStream(stream);
            }

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

            CloseCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (CloseAction != null)
                    CloseAction();
            });
        }
    }
}