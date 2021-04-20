using System;
using System.Drawing;

namespace Cash.Logic
{
    public class Product
    {
        public string Name { get; set; }

        public string BarCode { get; set; }

        public long BarCodeNumber => Int64.Parse(BarCode);

        public decimal Price { get; set; }

        public Image Picture { get; set; }
    }
}