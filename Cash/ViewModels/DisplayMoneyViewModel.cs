using Cash.Controls.ViewModels;
using Cash.Images;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cash.ViewModels
{
    public class DisplayMoneyViewModel : ViewModelBase
    {
        public GenericListViewModel<ConvertedMoneyItem> ConvertedMoney { get; private set; }

        private decimal sum = 0m;

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

        public void SetSum(decimal sum)
        {
            this.sum = sum;

            var sumToPay = sum;

            var allValues = MoneyImageList.GetValues().OrderByDescending(o => o);

            var result = new List<ConvertedMoneyItem>();

            while (sumToPay > 0)
            {
                var value = allValues.First(o => o <= sumToPay);

                var count = Math.Floor(sumToPay / value);

                result.Add(new ConvertedMoneyItem
                {
                    Value = value,
                    Count = (int)count,
                    MoneyImage = MoneyImageList.GetImageForValue(value)
                });

                sumToPay -= value * count;
            }

            ConvertedMoney.Items = result;
        }
    }
}
