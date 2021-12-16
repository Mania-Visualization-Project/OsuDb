using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class RecordDetailViewModel
    {
        public ContentDialog Dialog { get; set; } = null!;

        public ReplayModel Replay { get; set; } = null!;
    }
}
