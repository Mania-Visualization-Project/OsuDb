using OsuDb.ReplayMasterUI.Services;
using System.IO;
using System.Windows.Input;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class HomeViewModel
    {
        public HomeViewModel(Config config, DependencyChecker dependency)
        {
            this.config = config;
            this.dependencyChecker = dependency;
        }

        public string OsuRootPath => string.IsNullOrEmpty(config.OsuRootPath) ? "未找到" : config.OsuRootPath;

        public string OsuBeatmapDbStatus => File.Exists(config.OsuBeatmapDbPath) ? "存在" : "不存在";

        public string OsuScoreDbStatus => File.Exists(config.OsuScoreDbPath) ? "存在" : "不存在";

        public string JreStatus => dependencyChecker.IsJreInstalled ? "已安装" : "未安装";

        public string EncoderStatus => dependencyChecker.IsEncoderExsists ? "存在" : "未找到";

        public string ReplayMasterStatus => dependencyChecker.IsReplayMasterExsists ? "存在" : "未找到";

        public void SetOsuRootPath(string path)
        {
            if (!Directory.Exists(path)) return;
            config.OsuRootPath = path;
        }

        private readonly Config config;
        private readonly DependencyChecker dependencyChecker;
    }
}
