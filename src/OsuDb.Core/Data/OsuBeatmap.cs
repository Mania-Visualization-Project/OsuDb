using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.Core.Data
{
    public class OsuBeatmap
    {
        public string Artist { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Creator { get; set; } = string.Empty;

        public string Difficulty { get; set; } = string.Empty;

        public string AudioFileName { get; set; } = string.Empty;

        public string Md5 { get; set; } = string.Empty;

        public string BeatmapFileName { get; set; } = string.Empty;

        public GameMode GameMode { get; set; }

        public string FolderName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Artist} - {Title}, {Difficulty}, {GameMode}, {Md5}";
        }
    }

    public enum GameMode : byte
    {
        Std, Catch, Taiko, Mania
    }
}
