using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cash.Views
{
    public class ShoppingActions : UserControl
    {
        public ShoppingActions()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
