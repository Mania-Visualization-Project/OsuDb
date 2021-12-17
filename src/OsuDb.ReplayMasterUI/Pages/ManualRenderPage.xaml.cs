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
    public sealed partial class ManualRenderPage : Page
    {
        public ManualRenderPage()
        {
            this.InitializeComponent();
            window = DI.GetService<MainWindow>();
            renderService = DI.GetService<RenderViewModel>();
        }

        private readonly MainWindow window;
        private readonly RenderViewModel renderService;

        private void Render_Click(object sender, RoutedEventArgs e)
        {
            renderService.AddRenderRequest(BeatmapPath.Text, RecordPath.Text);
        }

        private async void SelectBeatmap_Click(object sender, RoutedEventArgs e)
        {
            // osu/osz/mc/mcz/zip
            var file = await window.BrowseSingleFile(new string[] { ".osu", ".osz", ".mc", ".mcz", ".zip" });
            if (file is null) return;

            BeatmapPath.Text = file.Path;
        }

        private async void SelectRecord_Click(object sender, RoutedEventArgs e)
        {
            // osr/mr
            var file = await window.BrowseSingleFile(new string[] { ".osr", ".mr" });
            if (file is null) return;

            RecordPath.Text = file.Path;
        }
    }
}
