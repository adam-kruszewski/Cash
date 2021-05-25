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

            var itemType = GetItemType();

            if (!mainGrid.Children.Any())
            {
                RunForEachColumn(itemType, (p, i) => AddColumnControl(p, i));
            }
        }

        private Type GetItemType()
        {
            return DataContext.GetType().GetGenericArguments().Single();
        }

        private void AddColumnControl(PropertyInfo p, int i)
        {
            object value = GetRowModel();

            var valueToDisplay = p.GetValue(value);

            IControl newControl;

            if (p.PropertyType == typeof(System.Drawing.Image))
            {
                newControl = PrepareImageControl(i, valueToDisplay as System.Drawing.Image);
            }
            else
            {
                newControl = PrepareTextControl(i, valueToDisplay, p);
            }

            newControl.Classes = new Classes("GridColumn");
            newControl.Classes.Add(p.Name);
            newControl.SetValue(Avalonia.Controls.Grid.RowProperty, 0);
            newControl.SetValue(Avalonia.Controls.Grid.ColumnProperty, i);

            mainGrid.Children.Insert(i, newControl);
        }

        private object GetRowModel()
        {
            var valueProperty = DataContext.GetType().GetProperty("Value");

            var value = valueProperty.GetValue(DataContext);
            return value;
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

        private TextBlock PrepareTextControl(int i, object valueToDisplay, PropertyInfo property)
        {
            var textControl = new TextBlock();

            textControl.Text = PrepareTextToDisplay(property, valueToDisplay, textControl);
            textControl.Margin = Thickness.Parse("3");

            return textControl;
        }

        private string PrepareTextToDisplay(PropertyInfo property, object valueToDisplay, TextBlock textControl)
        {
            var textValue = valueToDisplay?.ToString() ?? "";

            if (property.GetCustomAttribute<DisplayTextFunctionAttribute>() != null)
            {
                var displayMethodAttribute = property.GetCustomAttribute<DisplayTextFunctionAttribute>();

                if (!string.IsNullOrEmpty(displayMethodAttribute.MethodNameForDisplay))
                {
                    MethodInfo method = FindMethodForDisplayValue(displayMethodAttribute);
                    return (string)method.Invoke(DataContext, new object[] { valueToDisplay, GetRowModel() });
                }
            }

            return textValue;
        }

        private MethodInfo FindMethodForDisplayValue(DisplayTextFunctionAttribute displayMethodAttribute)
        {
            return GetItemType()
                .GetMethod(
                    displayMethodAttribute.MethodNameForDisplay,
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
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
