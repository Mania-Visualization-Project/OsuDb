using OsuDb.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class OsuReplayModel
    {
        public string Title { get; set; } = string.Empty;

        public string Player { get; set; } = string.Empty;

        public string BeatmapMd5 { get; set; } = null!;

        public int Score { get; set; }

        public int Count300 { get; set; }

        public int Count100 { get; set; }

        public int Count50 { get; set; }

        public int Count300Plus { get; set; }

        public int Count200 { get; set; }

        public int CountMiss { get; set; }

        public int Combo { get; set; }

        public bool IsFullCombo { get; set; }

        public DateTime PlayTime { get; set; }

        public string AudioFileName { get; set; } = string.Empty;

        public string BeatmapFileName { get; set; } = string.Empty;

        public string FolderName { get; set; } = string.Empty;

        public GameMode Mode { get; set; }

        public string Description => $"Played by {Player}, {PlayTime:yyyy/MM/dd HH:mm:ss}";
    }
}
