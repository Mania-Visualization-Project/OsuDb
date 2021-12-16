using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OsuDb.ReplayMasterUI.Services;
using OsuDb.ReplayMasterUI.ViewModels;
using System;
using System.Diagnostics;

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

        private async void InstallOpenJdk_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button ?? throw new InvalidCastException();
            button.Content = "下载中...";
            button.IsEnabled = false;
            var downloader = DI.GetService<DownloadService>();
            var window = DI.GetService<MainWindow>();

            var progress = new Progress<(long, long)>(p =>
            {
                jdkDownloadProgress.Text = $"{(int)(p.Item1 / (double)p.Item2 * 100)}%";
            });

            var path = await downloader.DownloadMsOpenJdkAsync(progress);
            button.Content = "正在安装";

            var process = new Process();
            process.StartInfo.FileName = "msiexec";
            process.StartInfo.Arguments = string.Format($"/i \"{path}\"");
            process.Start();

            await process.WaitForExitAsync();

            var message = process.ExitCode == 0 ? "OpenJDK安装完成，请重新启动本应用！" : "OpenJDK安装失败，请重试或自行安装。";

            var dialog = new ContentDialog
            {
                Title = "操作结果",
                Content = message,
                CloseButtonText = "确定"
            };
            window.ShowDialog(dialog);

            button.Content = "下载OpenJDK";
            button.IsEnabled = true;
        }
    }
}
