using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Cash.Controls.GenericListControl;
using Cash.Controls.ViewModels;
using System;
using System.Linq;

namespace Cash.Controls
{
    public partial class GenericList : UserControl
    {
        private Grid headerGrid;

        private Type typeOfRecord;

        public GenericList()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            headerGrid = this.FindControl<Grid>("HeaderGrid");
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            var t1 = typeof(GenericListViewModel<>);


            var dataContextType = DataContext?.GetType()?.GetGenericTypeDefinition();

            if (dataContextType != null && dataContextType == t1)
            {
                var itemType = DataContext.GetType().GetGenericArguments().First();

                if (typeOfRecord != itemType)
                    GenerateHeader(itemType);
            }

            //if (DataContext is typeof(GenericListViewModel<>))
            //{

            //}
        }

        private void GenerateHeader(Type itemType)
        {
            typeOfRecord = itemType;

            headerGrid.Children.Clear();
            headerGrid.ColumnDefinitions.Clear();

            foreach (var property in itemType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition(100, GridUnitType.Pixel));
                var h = new TextBlock();
                h.Text = GetHeaderText(property);
                h.SetValue(Avalonia.Controls.Grid.RowProperty, 0);
                h.SetValue(Avalonia.Controls.Grid.ColumnProperty, headerGrid.Children.Count);
                headerGrid.Children.Insert(headerGrid.Children.Count, h);
                h.Classes = new Classes("GridHeader");
                h.Margin = Thickness.Parse("3");
            }
        }

        private string GetHeaderText(System.Reflection.PropertyInfo property)
        {
            var headerAttribute = property.GetCustomAttributes(typeof(HeaderAttribute), true).SingleOrDefault() as HeaderAttribute;

            if (headerAttribute != null && headerAttribute.Text != null)
                return headerAttribute.Text;

            return property.Name;
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var pen = new Pen(Color.FromRgb(255, 0, 0).ToUint32());

            context.DrawLine(pen, new Point(0, 0), new Point(0, Bounds.Height));
            foreach (var column in headerGrid.Children)
            {
                var x1 = column.Bounds.X + column.Bounds.Width;
                context.DrawLine(pen, new Point(x1, 0), new Point(x1, Bounds.Height));
            }

            //context.DrawLine(new Pen(Color.FromRgb(255, 0, 0).ToUint32()), new Point(0, 10), new Point(100, 10));
        }
    }
}
