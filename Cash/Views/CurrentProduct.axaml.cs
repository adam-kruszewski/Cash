using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cash.Views
{
    public class CurrentProduct : UserControl
    {
        public CurrentProduct()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
