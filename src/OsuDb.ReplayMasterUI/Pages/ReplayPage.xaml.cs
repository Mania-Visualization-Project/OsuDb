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
            InitDataContext();
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
            if (viewModel is null) return;
            viewModel.Filter(replays =>
            {
                var result = replays;
                if (FcOnly.IsChecked == true)
                    result = result.Where(r => r.IsFullCombo);
                var tag = (FilterMode.SelectedItem as ComboBoxItem)?.Tag as string;
                if (tag == "mania")
                    result = result.Where(r => r.Mode is Core.Data.GameMode.Mania);
                if (tag == "taiko")
                    result = result.Where(r => r.Mode is Core.Data.GameMode.Taiko);
                return result;
            });
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            DataContext = null;
            await viewModel.RefreshAsync(new Progress<(int, int)>());
            await Task.Delay(300);
            DataContext = viewModel;
            DoFilter();
            progressRing.IsActive = false;
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var record = e.ClickedItem as ReplayModel ?? throw new InvalidCastException();
            var viewModel = DI.GetService<RecordDetailViewModel>();
            viewModel.Replay = record;

            var dialog = new RecordDetailDialog(viewModel);

            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }
    }
}
