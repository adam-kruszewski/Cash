using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Cash.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace Cash.Views
{
    public class ShoppingActions : ReactiveUserControl<ShoppingActionViewModel>
    {
        public ShoppingActions()
        {
            InitializeComponent();

            this.WhenActivated(d => d((DataContext as ShoppingActionViewModel).ShowMoney.RegisterHandler(ShowMoneyDialog)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task ShowMoneyDialog(InteractionContext<DisplayMoneyViewModel, DisplayMoneyViewModel> interaction)
        {
            var dialog = new DisplayMoney();
            dialog.DataContext = interaction.Input;

            
            var result = await dialog.ShowDialog<DisplayMoneyViewModel>(VisualRoot as Window);
            interaction.SetOutput(result);
        }
    }
}
