using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cash.ViewModels;
using System;

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
            try
            {
                AvaloniaXamlLoader.Load(this);
            }catch (Exception ex)
            {
                throw;
            }
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            var viewModel = (PrintCodesViewModel)DataContext;
            viewModel.CloseAction = () => Close();
        }
    }
}
