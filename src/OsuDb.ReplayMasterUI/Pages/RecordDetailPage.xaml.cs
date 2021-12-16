using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using OsuDb.ReplayMasterUI.ViewModels;
using OsuDb.ReplayMasterUI.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OsuDb.ReplayMasterUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecordDetailPage : Page
    {
        public RecordDetailPage()
        {
            this.InitializeComponent();
        }

        private RecordDetailViewModel viewModel = null!;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = e.Parameter as RecordDetailViewModel ?? throw new InvalidCastException();
            this.viewModel = viewModel;
            DataContext = viewModel;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Dialog.Hide();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var renderViewModel = DI.GetService<RenderViewModel>();
            renderViewModel.AddRenderRequest(viewModel.Replay);
        }
    }
}
