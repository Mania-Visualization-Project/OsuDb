using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using OsuDb.ReplayMasterUI.Services;
using OsuDb.ReplayMasterUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OsuDb.ReplayMasterUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReplayPage : Page
    {
        public ReplayPage()
        {
            this.InitializeComponent();
            viewModel = DI.GetService<ReplayViewModel>();
            config = DI.GetService<Config>();
        }

        private void HandleNotInited()
        {
            Refresh.IsEnabled = false;
            FcOnly.IsEnabled = false;
            FilterMode.IsEnabled = false;
            SearchBar.IsEnabled = false;
            progressRing.IsActive = false;
            var window = DI.GetService<MainWindow>();
            var dialog = new ContentDialog
            {
                Content = "未找到Osu数据文件，请先配置！",
                CloseButtonText = "确定",
                Title = "初始化失败"
            };
            dialog.Closed += (s, e) =>
            {
                Frame.Navigate(typeof(HomePage));
            };
            window.ShowDialog(dialog);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (config.OsuReady)
                InitDataContext();
            else
                HandleNotInited();
        }

        private async void InitDataContext()
        {
            progressRing.IsActive = true;
            await viewModel.RefreshAsync(new Progress<(int, int)>());
            await Task.Delay(300);
            DataContext = viewModel;
            progressRing.IsActive = false;
        }

        private readonly ReplayViewModel viewModel;
        private readonly Config config;

        private void FcOnly_Checked(object sender, RoutedEventArgs e)
        {
            DoFilter();
        }

        private void FilterMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoFilter();
        }

        private void DoFilter()
        {
            if (viewModel is null || DataContext is null) return;
            viewModel.Filter(replays =>
            {
                var result = replays;
                if (FcOnly.IsChecked == true)
                    result = result.Where(r => r.IsFullCombo);
                var tag = (FilterMode.SelectedItem as ComboBoxItem)?.Tag as string;
                if (tag == "std")
                    result = result.Where(r => r.Mode is Core.Data.GameMode.Std);
                if (tag == "mania")
                    result = result.Where(r => r.Mode is Core.Data.GameMode.Mania);
                if (tag == "taiko")
                    result = result.Where(r => r.Mode is Core.Data.GameMode.Taiko);
                return result;
            });
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            SearchBar.Text = String.Empty;
            progressRing.IsActive = true;
            DataContext = null;
            await viewModel.RefreshAsync(new Progress<(int, int)>());
            await Task.Delay(300);
            DataContext = viewModel;
            DoFilter();
            progressRing.IsActive = false;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel is null) return;
            var textbox = sender as TextBox;
            var word = textbox!.Text;
            if (string.IsNullOrEmpty(word)) return;
            viewModel.Filter(replays =>
            {
                var keywords = word.Split(' ');
                foreach (var keyword in keywords)
                {
                    replays = replays.Where(r => r.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                }
                return replays;
            });
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var record = e.ClickedItem as OsuReplayModel ?? throw new InvalidCastException();
            var viewModel = DI.GetService<RecordDetailViewModel>();
            viewModel.Replay = record;

            var dialog = new RecordDetailDialog(viewModel);

            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }
    }
}
