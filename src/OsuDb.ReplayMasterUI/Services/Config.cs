using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    public class Config
    {
        public string OsuRootPath { get; set; } = string.Empty;

        public string EncoderPath => Path.Combine(CurrentPath, "ffmpeg.exe");

        public string ReplayMasterPath => Path.Combine(CurrentPath, "ManiaReplayMaster.jar");

        public string OsuScoreDbPath => Path.Combine(OsuRootPath, "scores.db");

        public string OsuBeatmapDbPath => Path.Combine(OsuRootPath, "osu!.db");

        public bool OsuReady => File.Exists(OsuScoreDbPath) && File.Exists(OsuBeatmapDbPath);

        public string CurrentPath => currentPath ??= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        private string currentPath = null!;
    }
}
