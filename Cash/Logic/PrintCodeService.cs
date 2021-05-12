using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.BarCodes;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cash.Logic
{
    class PrintCodeService : IPrintCodesService
    {
        private const int Margin = 10;
        private const int CodeWidth = 100;
        private const int CodeHeight = 50;

        public byte[] PrintCodesToPdf(IEnumerable<ProductCodeToPrint> codes)
        {
            try
            {
                var itemsToPrint = codes.SelectMany(o => MultiplyCode(o));

                var pdfDocument = new PdfDocument();
                var font = new XFont("OpenSans", 20, XFontStyle.Bold);

                PdfPage page;
                XGraphics gfx;
                AddNewPage(pdfDocument, out page, out gfx);

                int currentXPos, currentYPos;
                InitPositionOnNewPage(out currentXPos, out currentYPos);

                foreach (var printItem in itemsToPrint)
                {
                    if (currentYPos + CodeHeight > page.Height)
                    {
                        AddNewPage(pdfDocument, out page, out gfx);
                        InitPositionOnNewPage(out currentXPos, out currentYPos);
                    }

                    PrintBarCode(gfx, currentXPos, currentYPos, printItem);

                    PrintDescription(font, gfx, currentXPos, currentYPos, printItem);

                    currentYPos += CodeHeight + Margin;
                }

                using (var stream = new MemoryStream())
                {
                    pdfDocument.Save(stream);
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void PrintDescription(
            XFont font,
            XGraphics gfx,
            int currentXPos,
            int currentYPos,
            PrintItem printItem)
        {
            gfx.DrawString(
                printItem.Description,
                font,
                XBrushes.Black,
                new XPoint(currentXPos + Margin + CodeWidth, currentYPos - CodeHeight / 2));
        }

        private void PrintBarCode(
            XGraphics gfx,
            int currentXPos,
            int currentYPos,
            PrintItem printItem)
        {
            Code3of9Standard barCode = new Code3of9Standard(printItem.Code, new XSize(CodeWidth, CodeHeight));
            barCode.TextLocation = TextLocation.Below;
            barCode.Anchor = AnchorType.BottomLeft;
            gfx.DrawBarCode(barCode, new XPoint(currentXPos, currentYPos));
        }

        private void InitPositionOnNewPage(
            out int currentXPos,
            out int currentYPos)
        {
            currentXPos = 10;
            currentYPos = 10 + CodeHeight;
        }

        private static void AddNewPage(PdfDocument pdfDocument, out PdfPage page, out XGraphics gfx)
        {
            page = pdfDocument.AddPage();
            gfx = XGraphics.FromPdfPage(page);
        }

        private IEnumerable<PrintItem> MultiplyCode(ProductCodeToPrint o)
        {
            for (int i = 0; i < o.Count; i++)
            {
                yield return new PrintItem(o.Code, o.Description);
            }
        }

        private class PrintItem
        {
            public string Code { get; private set; }

            public string Description { get; private set; }

            public PrintItem(
                string code,
                string description)
            {
                Code = code;
                Description = description;
            }
        }
    }
}
