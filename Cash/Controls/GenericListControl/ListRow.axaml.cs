using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cash.Controls.ViewModels;
using System;
using System.Linq;
using System.Reflection;

namespace Cash.Controls.GenericListControl
{
    public partial class ListRow : UserControl
    {
        public string ColumnDefinition
        {
            set
            {
                if (!string.IsNullOrEmpty(value) && mainGrid != null)
                {
                    mainGrid.ColumnDefinitions = ColumnDefinitions.Parse(value);
                }
            }
        }

        private Grid mainGrid;

        public ListRow()
        {
            InitializeComponent();
            mainGrid = this.FindControl<Grid>("MainGrid");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            var modelWithColumnDefinition = DataContext as IHasColumnDefinition;
            if (modelWithColumnDefinition != null)
            {
                ColumnDefinition = modelWithColumnDefinition.ColumnDefinition;
            }

            var itemType = DataContext.GetType().GetGenericArguments().Single();

            if (!mainGrid.Children.Any())
            {
                RunForEachColumn(itemType, (p,  i) => AddColumnControl(p, i));
            }
        }

        private void AddColumnControl(PropertyInfo p, int i)
        {
            var valueProperty = DataContext.GetType().GetProperty("Value");

            var value = valueProperty.GetValue(DataContext);

            var valueToDisplay = p.GetValue(value);


            var textControl = new TextBlock();
            textControl.Text = valueToDisplay?.ToString() ?? "";
            textControl.SetValue(Avalonia.Controls.Grid.RowProperty, 0);
            textControl.SetValue(Avalonia.Controls.Grid.ColumnProperty, i);
            mainGrid.Children.Insert(i, textControl);
            textControl.Classes = new Classes("GridColumn");
            textControl.Margin = Thickness.Parse("3");
        }

        private void RunForEachColumn(Type itemType, Action<PropertyInfo, int> action)
        {
            var properties = itemType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            for (int i = 0; i < properties.Length; i++)
            {
                action(properties[i], i);
            }
        }
    }
}
