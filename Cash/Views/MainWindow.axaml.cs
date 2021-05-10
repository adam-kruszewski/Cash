using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Cash.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace Cash.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => 
                d(
                    ViewModel.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task DoShowDialogAsync(InteractionContext<PrintCodesViewModel, PrintCodesViewModel> interaction)
        {
            var dialog = new PrintCodesWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<PrintCodesViewModel>(this);
            interaction.SetOutput(result);
        }

    }
}
