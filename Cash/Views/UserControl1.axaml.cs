using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cash.Views
{
    public class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
