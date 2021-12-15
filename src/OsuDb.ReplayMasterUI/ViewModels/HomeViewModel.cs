using OsuDb.ReplayMasterUI.Services;
using System.IO;
using System.Windows.Input;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class HomeViewModel
    {
        public HomeViewModel(Config config)
        {
            this.config = config;
        }

        public string OsuRootPath => string.IsNullOrEmpty(config.OsuRootPath) ? "未找到" : config.OsuRootPath;

        public string OsuBeatmapDbStatus => File.Exists(config.OsuBeatmapDbPath) ? "存在" : "不存在";

        public string OsuScoreDbStatus => File.Exists(config.OsuScoreDbPath) ? "存在" : "不存在";

        public void SetOsuRootPath(string path)
        {
            if (!Directory.Exists(path)) return;
            config.OsuRootPath = path;
        }

        private readonly Config config;
    }
}
