using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cash.ViewModels;
using System;

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

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            var viewModel = (CurrentProductViewModel)DataContext;

            viewModel.GainFocusOnBarCodeFieldAction =
                () =>
                {
                    var barCodeControl = this.FindControl<TextBox>("BarCode");
                    barCodeControl.Focus();
                };
        }

        
    }
}
