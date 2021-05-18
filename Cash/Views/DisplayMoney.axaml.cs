using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cash.Views
{
    public partial class DisplayMoney : Window
    {
        public DisplayMoney()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
