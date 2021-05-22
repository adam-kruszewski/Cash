using Cash.ViewModels;

namespace Cash.Controls.ViewModels
{
    public class GenericListRowViewModel<T> : ViewModelBase, IHasColumnDefinition
    {
        public T Value { get; set; }

        public string ColumnDefinition { get; set; }
    }
}
