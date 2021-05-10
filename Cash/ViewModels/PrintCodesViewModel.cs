using System;

namespace Cash.ViewModels
{
    public class PrintCodesViewModel : ViewModelBase
    {
        public string FilePath { get; set; }

        public PrintCodesViewModel()
        {
            FilePath = $"BarCodes\\barcodes-{DateTime.Now.ToFileTime()}.pdf";
        }
    }
}
