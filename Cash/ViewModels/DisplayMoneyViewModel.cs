using Cash.Controls.ViewModels;
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

            for (int i = 0; i < 5; i++)
                tempList.Add(new ConvertedMoneyItem
                {
                    Count = i + 1,
                    Value = (i + 1) * 10
                });

            convertedMoney.Items = tempList;
        }
    }
}
