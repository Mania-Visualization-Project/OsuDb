using OsuDb.ReplayMasterUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class RenderService
    {
        public RenderService(Config config, DependencyChecker checker)
        {
            this.config = config;
            this.dependencyChecker = checker;
        }

        public int ParallelRenderConut { get; set; } = 1;

        private int renderingCount = 0;

        public async Task<(bool, string)> RenderAsync(ReplayModel replay, IProgress<int> progress)
        {
            var osrName = $"{replay.BeatmapMd5}-{replay.PlayTime.ToFileTimeUtc()}.osr";
            var osuPath = Path.Combine(config.OsuRootPath, "Songs", replay.FolderName, replay.BeatmapFileName);
            var osrPath = Path.Combine(config.OsuRootPath, "Data", "r", osrName);

            // check required files and dependencies.
            if (!File.Exists(osrPath) || !File.Exists(osuPath)) return (false, "回放文件丢失");
            var dependencyOk = dependencyChecker.IsEncoderExsists && 
                               dependencyChecker.IsJreInstalled && 
                               dependencyChecker.IsReplayMasterExsists;
            if (!dependencyOk) return (false, "程序组件缺失，无法渲染");

            // todo: check config file.

            // parallel render count limit.
            while (renderingCount >= ParallelRenderConut)
                await Task.Delay(500);
            renderingCount++;
            // run render.
            var message = string.Empty;
            var success = false;
            var argument = $"-jar \"{config.ReplayMasterPath}\" \"{osuPath}\" \"{osrPath}\" \"{config.ReplayMasterConfigPath}\"";
            var process = new Process();
            process.StartInfo.FileName = "java.exe";
            process.StartInfo.Arguments = argument;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data is null) return;
                var msg = e.Data;
                if (msg.StartsWith("progress"))
                {
                    var number = (int)double.Parse(msg[10..]);
                    progress.Report(number);
                }
                else if (msg.StartsWith("out"))
                {
                    message = msg[5..];
                    success = true;
                }
                else if (msg.StartsWith("error"))
                {
                    message = msg[7..];
                    success = false;
                }
            };
            process.Start();
            process.BeginOutputReadLine();
            await process.WaitForExitAsync();
            renderingCount--;
            // var err = await process.StandardError.ReadToEndAsync();
            if (process.ExitCode == 0)
                 return (success, message);
            else
                return (false, "渲染程序异常退出");
        }

        private readonly Config config;
        private readonly DependencyChecker dependencyChecker;
    }
}
