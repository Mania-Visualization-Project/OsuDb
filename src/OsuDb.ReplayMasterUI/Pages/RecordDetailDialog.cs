using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using OsuDb.ReplayMasterUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Pages
{
    internal class RecordDetailDialog : ContentDialog
    {
        public RecordDetailDialog(RecordDetailViewModel viewModel) : base()
        {
            this.viewModel = viewModel;
            this.Title = "成绩详情";
            var frame = new Frame();
            frame.Background = new SolidColorBrush(Colors.Transparent);
            this.Content = frame;

            viewModel.Dialog = this;
            frame.Navigate(typeof(RecordDetailPage), viewModel);
        }

        private readonly RecordDetailViewModel viewModel;
    }
}
