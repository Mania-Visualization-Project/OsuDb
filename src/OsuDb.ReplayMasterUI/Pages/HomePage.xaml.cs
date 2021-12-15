using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OsuDb.ReplayMasterUI.Services;
using OsuDb.ReplayMasterUI.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OsuDb.ReplayMasterUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            viewModel = DI.GetService<HomeViewModel>();
            mainWindow = DI.GetService<MainWindow>();
            DataContext = viewModel;
        }

        private readonly HomeViewModel viewModel;
        private readonly MainWindow mainWindow;

        private async void SelectPath_Click(object sender, RoutedEventArgs e)
        {
            var folder = await mainWindow.BrowseSingleFolder();

            if (folder is null) return;

            viewModel.SetOsuRootPath(folder.Path);
            DataContext = null;
            DataContext = viewModel;
        }
    }
}
