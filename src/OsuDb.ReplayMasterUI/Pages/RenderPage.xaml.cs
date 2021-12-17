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
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OsuDb.ReplayMasterUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RenderPage : Page
    {
        public RenderPage()
        {
            this.InitializeComponent();
            viewModel = DI.GetService<RenderViewModel>();
            DataContext = viewModel;
        }

        private readonly RenderViewModel viewModel;

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as HyperlinkButton ?? throw new InvalidCastException();
            var path = button.Tag as string;

            Process.Start("explorer.exe", $"/select, \"{path}\"");
        }
    }
}
