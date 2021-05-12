using System.Collections.Generic;

namespace Cash.Logic
{
    public interface IPrintCodesService
    {
        byte[] PrintCodesToPdf(IEnumerable<ProductCodeToPrint> codes);
    }
}
