using Cash.Controls.GenericListControl;
using System;
using System.Drawing;

namespace Cash.ViewModels
{
    public class ConvertedMoneyItem
    {
        [Header("Wartość")]
        [DisplayTextFunction(MethodNameForDisplay = "GetValueText")]
        public decimal Value { get; set; }

        public Image MoneyImage { get; set; }

        public int Count { get; set; }

        private static string GetValueText(decimal column, ConvertedMoneyItem row)
        {
            var value = (decimal)column;

            if (value == Math.Round(value))
            {
                return $"{value} zł";
            }else
            {
                return $"{Math.Round(value * 100)} gr";
            }
        }
    }
}