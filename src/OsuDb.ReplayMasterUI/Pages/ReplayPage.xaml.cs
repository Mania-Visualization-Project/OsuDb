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
            if (!viewModel.DataInited)
            {
                progressRing.IsActive = true;
                await viewModel.RefreshAsync(new Progress<(int, int)>());
            }
            DataContext = viewModel;
            progressRing.IsActive = false;
        }

        private readonly ReplayViewModel viewModel;
    }
}
