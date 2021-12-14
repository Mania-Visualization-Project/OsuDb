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
        }

        private string osuRootDirectory = null!;

        private void GetOsuPath_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? throw new InvalidCastException();
            var locator = DI.GetService<IOsuLocator>();
            var dir = locator.GetOsuRootDirectory();
            osuRootDirectory = dir?.FullName!;
            log.Text += $"{button.Name}: {dir?.FullName}\n";
        }

        private void ReadReplays_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? throw new InvalidCastException();
            var reader = DI.GetService<OsuDbReader>();
            if (osuRootDirectory is null)
            {
                log.Text += $"{button.Name}: cant find osu! directory.\n";
                return;
            }
            var records = reader.ReadScores(Path.Combine(osuRootDirectory, "scores.db"));
            log.Text += "\n";
            foreach (var score in records.Scores)
            {
                log.Text += $"{score}\n";
            }
        }
    }
}
