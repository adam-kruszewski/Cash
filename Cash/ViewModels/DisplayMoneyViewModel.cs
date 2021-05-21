using Cash.Controls.ViewModels;

namespace Cash.ViewModels
{
    public class DisplayMoneyViewModel : ViewModelBase
    {
        public GenericListViewModel<ConvertedMoneyItem> ConvertedMoney { get; private set; }

        public DisplayMoneyViewModel(
            GenericListViewModel<ConvertedMoneyItem> convertedMoney)
        {
            ConvertedMoney = convertedMoney;
        }
    }
}
