using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using OsuDb.Core;
using OsuDb.ReplayMasterUI.Services;
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
    public sealed partial class TestPage : Page
    {
        public TestPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }


        public string Log
        {
            get { return (string)GetValue(LogProperty); }
            set { SetValue(LogProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Log.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogProperty =
            DependencyProperty.Register("Log", typeof(string), typeof(TestPage), new PropertyMetadata(""));


        private string osuRootDirectory = null!;

        private void GetOsuPath_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? throw new InvalidCastException();
            var locator = DI.GetService<IOsuLocator>();
            var dir = locator.GetOsuRootDirectory();
            osuRootDirectory = dir?.FullName!;
            Log += $"{button.Name}: {dir?.FullName}\n";
        }

        private async void ReadReplays_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? throw new InvalidCastException();
            var reader = DI.GetService<OsuDbReader>();
            if (osuRootDirectory is null)
            {
                Log += $"{button.Name}: cant find osu! directory.\n";
                return;
            }

            var progress = new Progress<(int, int)>(p =>
            {
                processIndicator.Text = $"{p.Item1}/{p.Item2}\n";
            });

            button.IsEnabled = false;

            try
            {
                var scoresDb = await reader.ReadScores(Path.Combine(osuRootDirectory, "scores.db"), progress);
                Log += "\n";
                foreach (var score in scoresDb.Scores)
                {
                    Log += $"{score}\n";
                }
            }
            finally { button.IsEnabled = true; }
        }

        private async void ReadBeadMaps_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? throw new InvalidCastException();
            var reader = DI.GetService<OsuDbReader>();
            if (osuRootDirectory is null)
            {
                Log += $"{button.Name}: cant find osu! directory.\n";
                return;
            }
            var progress = new Progress<(int, int)>(p =>
            {
                processIndicator.Text = $"{p.Item1}/{p.Item2}\n";
            });

            button.IsEnabled = false;

            try
            {

                var beatmaps = await reader.ReadBeatmapsAsync(Path.Combine(osuRootDirectory, "osu!.db"), progress);
                Log += "\n";
                Log += $"{button.Name}: read {beatmaps.Beatmaps.Count()} maps:\n";
                foreach (var beatmap in beatmaps.Beatmaps)
                {
                    Log += $"{beatmap}\n";
                }
            }
            finally { button.IsEnabled = true; }
        }
    }
}
