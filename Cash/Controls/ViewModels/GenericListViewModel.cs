using Cash.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace Cash.Controls.ViewModels
{
    public class GenericListViewModel<T> : ViewModelBase, ICanSetColumnDefinition
    {
        public string Header => "Here will be list header";

        private string columnDefinition;

        public string ColumnDefinition
        {
            set
            {
                columnDefinition = value;
                foreach (var row in rows)
                {
                    row.ColumnDefinition = columnDefinition;
                }
                this.RaisePropertyChanged("Rows");
            }
        }

        public IList<T> Items
        {
            set
            {
                rows = value.Select(o => new GenericListRowViewModel<T>
                {
                    Value = o,
                    ColumnDefinition = null
                }).ToList();
                this.RaisePropertyChanged("Rows");
            }
        }

        private List<GenericListRowViewModel<T>> rows;
        public IList<GenericListRowViewModel<T>> Rows
        {
            get { return rows; }
        }

        public GenericListViewModel()
        {
            Items = new List<T>();
            rows = new List<GenericListRowViewModel<T>>();
        }
    }
}
