using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OsuDb.ReplayMasterUI.Services
{
    public class Config
    {
        public string OsuRootPath { get; set; } = string.Empty;

        public string EncoderPath => Path.Combine(Path.GetTempPath(), "ffmpeg.exe");

        public string ReplayMasterPath => Path.Combine(Path.GetTempPath(), "ManiaReplayMaster.jar");

        public string VideoOutputDir { get; set; } = @"C:\mrmOutput";

        public string OsuScoreDbPath => Path.Combine(OsuRootPath, "scores.db");

        public string OsuBeatmapDbPath => Path.Combine(OsuRootPath, "osu!.db");

        public string ConfigsJsonPath => Path.Combine(CurrentPath, "configs.json");

        public bool OsuReady => File.Exists(OsuScoreDbPath) && File.Exists(OsuBeatmapDbPath);

        public string CurrentPath => currentPath ??= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        private string currentPath = null!;
    }
}
