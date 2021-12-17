using OsuDb.ReplayMasterUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class RenderViewModel
    {
        public RenderViewModel(RenderService render, Config config)
        {
            this.config = config;
            renderService = render;
        }

        public ObservableCollection<RenderModel> RenderModels { get; } = new();
        private readonly RenderService renderService;
        private readonly Config config;

        public async void AddRenderRequest(string beatmapPath, string replayPath)
        {
            if (!File.Exists(beatmapPath) || !File.Exists(replayPath)) return;
            // todo: prevent rendering same replays at one time

            var beatmapFile = new FileInfo(beatmapPath);

            var renderItem = new RenderModel
            {
                StartTime = DateTime.Now.ToShortTimeString(),
                Title = beatmapFile.Name.Replace(beatmapFile.Extension, string.Empty),
            };
            RenderModels.Add(renderItem);

            var progress = new Progress<int>(p =>
            {
                renderItem.Progress = p;
            });
            var outputName = $"{renderItem.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
            var (success, msg) = await renderService.RenderAsync(beatmapPath, replayPath, "default", outputName, progress);
            if (!success)
            {
                renderItem.IsFailed = true;
                renderItem.ErrorMessage = msg;
                return;
            }
            renderItem.Finished = true;
            renderItem.OutputFilePath = msg;
        }

        public void AddRenderRequest(OsuReplayModel replay)
        {
            var osrName = $"{replay.BeatmapMd5}-{replay.PlayTime.ToFileTimeUtc()}.osr";
            var osuPath = Path.Combine(config.OsuRootPath, "Songs", replay.FolderName, replay.BeatmapFileName);
            var osrPath = Path.Combine(config.OsuRootPath, "Data", "r", osrName);
            AddRenderRequest(osuPath, osrPath);
        }
    }
}
