using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cash.Views
{
    public partial class PrintCodesWindow : Window
    {
        public PrintCodesWindow()
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
