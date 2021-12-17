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
        public RenderService(Config config, DependencyChecker checker, RenderOptionService renderOptions)
        {
            this.config = config;
            this.renderOptionService = renderOptions;
            this.dependencyChecker = checker;
        }

        public int ParallelRenderConut { get; set; } = 3;

        private int renderingCount = 0;

        public async Task<(bool, string)> RenderAsync(string beatmapPath, string replayPath, 
            string configName, string outputFileName, IProgress<int> progress)
        {
            // check required files and dependencies.
            if (!File.Exists(replayPath) || !File.Exists(beatmapPath)) return (false, "回放文件丢失");
            var dependencyOk = dependencyChecker.IsEncoderExsists &&
                               dependencyChecker.IsJreInstalled &&
                               dependencyChecker.IsReplayMasterExsists;
            if (!dependencyOk) return (false, "程序组件缺失，无法渲染");

            // parallel render count limit.
            while (renderingCount >= ParallelRenderConut)
                await Task.Delay(500);
            renderingCount++;
            // run render.
            var message = string.Empty;
            var success = false;
            var argument = $"-jar \"{config.ReplayMasterPath}\" \"{beatmapPath}\" \"{replayPath}\" \"{renderOptionService.GetRenderOptionsFile(configName)}\"";
            var process = new Process();
            process.StartInfo.WorkingDirectory = config.CurrentPath;
            process.StartInfo.FileName = "java.exe";
            process.StartInfo.Arguments = argument;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.Verb = "runas";
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
            if (process.ExitCode == 0)
            {
                if (!Directory.Exists(config.VideoOutputDir))
                    Directory.CreateDirectory(config.VideoOutputDir);
                var finalOutputPath = Path.Combine(config.VideoOutputDir, outputFileName);
                File.Move(message, finalOutputPath, true);
                return (success, finalOutputPath);
            }
            else
            {
                var err = await process.StandardError.ReadToEndAsync();
                using var file = File.OpenWrite(Path.Combine(config.VideoOutputDir, "crash_log.txt"));
                using var writer = new StreamWriter(file);
                writer.WriteLine(err);
                writer.Flush();
                writer.Close();
                return (false, "渲染程序异常退出");
            }  
        }

        private readonly Config config;
        private readonly DependencyChecker dependencyChecker;
        private readonly RenderOptionService renderOptionService;
    }
}
