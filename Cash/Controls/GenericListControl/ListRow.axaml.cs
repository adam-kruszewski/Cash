using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Cash.Controls.ViewModels;
using Cash.Converters;
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
                RunForEachColumn(itemType, (p, i) => AddColumnControl(p, i));
            }
        }

        private void AddColumnControl(PropertyInfo p, int i)
        {
            var valueProperty = DataContext.GetType().GetProperty("Value");

            var value = valueProperty.GetValue(DataContext);

            var valueToDisplay = p.GetValue(value);

            IControl newControl;

            if (p.PropertyType == typeof(System.Drawing.Image))
            {
                newControl = PrepareImageControl(i, valueToDisplay as System.Drawing.Image);
            }
            else
            {
                newControl = PrepareTextControl(i, valueToDisplay);
            }

            newControl.Classes = new Classes("GridColumn");
            newControl.Classes.Add(p.Name);
            newControl.SetValue(Avalonia.Controls.Grid.RowProperty, 0);
            newControl.SetValue(Avalonia.Controls.Grid.ColumnProperty, i);

            mainGrid.Children.Insert(i, newControl);
        }

        private Image PrepareImageControl(int i, System.Drawing.Image imageToDisplay)
        {
            var imageControl = new Image();

            var imageConverter = new ImageConverter();
            var convertedSource = imageConverter.Convert(imageToDisplay, null, null, null);
            imageControl.Source = convertedSource as IImage;
            imageControl.Margin = Thickness.Parse("3");

            return imageControl;
        }

        private TextBlock PrepareTextControl(int i, object valueToDisplay)
        {
            var textControl = new TextBlock();
            textControl.Text = valueToDisplay?.ToString() ?? "";
            textControl.Margin = Thickness.Parse("3");

            return textControl;
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
