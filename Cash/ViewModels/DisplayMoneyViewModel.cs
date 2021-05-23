using Cash.Controls.ViewModels;
using Cash.Images;
using System.Collections.Generic;

namespace Cash.ViewModels
{
    public class DisplayMoneyViewModel : ViewModelBase
    {
        public GenericListViewModel<ConvertedMoneyItem> ConvertedMoney { get; private set; }

        public DisplayMoneyViewModel(
            GenericListViewModel<ConvertedMoneyItem> convertedMoney)
        {
            ConvertedMoney = convertedMoney;

            var tempList = new List<ConvertedMoneyItem>();

            var values = new decimal[] { 0.5m, 1m, 2m, 5m, 10m, 20m };

            for (int i = 0; i < 5; i++)
                tempList.Add(new ConvertedMoneyItem
                {
                    Count = i + 1,
                    Value = values[i],
                    MoneyImage = MoneyImageList.GetImageForValue(values[i])
                });

            convertedMoney.Items = tempList;
        }
    }
}
