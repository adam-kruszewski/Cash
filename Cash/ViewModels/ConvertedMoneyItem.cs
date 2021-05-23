using Cash.Controls.GenericListControl;
using System.Drawing;

namespace Cash.ViewModels
{
    public class ConvertedMoneyItem
    {
        [Header("Wartość")]
        public decimal Value { get; set; }

        public Image MoneyImage { get; set; }

        public int Count { get; set; }
    }
}
