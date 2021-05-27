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

        private ListBox listboxContent;


        private string customColumnDefinition;
        public string CustomColumnDefinition
        {
            get => customColumnDefinition;
            set
            {
                customColumnDefinition = value;
                SetColumnDefinitionInViewModelIfPossible();
            }
        }

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

            SetColumnDefinitionInViewModelIfPossible();
        }

        private void SetColumnDefinitionInViewModelIfPossible()
        {
            var canSetColumnDefinitionViewModel = DataContext as ICanSetColumnDefinition;

            if (canSetColumnDefinitionViewModel != null)
            {
                canSetColumnDefinitionViewModel.ColumnDefinition = CustomColumnDefinition;
            }
        }

        private void GenerateHeader(Type itemType)
        {
            typeOfRecord = itemType;

            headerGrid.Children.Clear();
            headerGrid.ColumnDefinitions.Clear();

            if (!string.IsNullOrEmpty(CustomColumnDefinition))
            {
                headerGrid.ColumnDefinitions = ColumnDefinitions.Parse(CustomColumnDefinition);
            }

            foreach (var property in itemType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                var headerTextBlock = new TextBlock();
                headerTextBlock.Text = GetHeaderText(property);
                headerTextBlock.SetValue(Avalonia.Controls.Grid.RowProperty, 0);
                headerTextBlock.SetValue(Avalonia.Controls.Grid.ColumnProperty, headerGrid.Children.Count);
                headerGrid.Children.Insert(headerGrid.Children.Count, headerTextBlock);
                headerTextBlock.Classes = new Classes("GridHeader");
                headerTextBlock.Margin = Thickness.Parse("3");
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

            DrawGridLines(context, headerGrid, Bounds);
        }

        private static void DrawGridLines(DrawingContext context, Grid gridControl, Rect controlBounds)
        {
            var pen = new Pen(Color.FromRgb(255, 0, 0).ToUint32());

            context.DrawLine(pen, new Point(0, 0), new Point(0, controlBounds.Height));
            foreach (var column in gridControl.Children)
            {
                var x1 = column.Bounds.X + column.Bounds.Width;
                context.DrawLine(pen, new Point(x1, 0), new Point(x1, controlBounds.Height));
            }
        }
    }
}
