using OsuDb.ReplayMasterUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class RenderViewModel
    {
        public RenderViewModel(RenderService render)
        {
            renderService = render;
        }

        public ObservableCollection<RenderModel> RenderModels { get; } = new();
        private readonly RenderService renderService;

        public async void AddRenderRequest(ReplayModel replay)
        {
            if (RenderModels.Where(x => !x.IsFailed && !x.Finished).Where(x => x.Replay == replay).Any())
                return;
            var renderItem = new RenderModel
            {
                StartTime = DateTime.Now.ToShortTimeString(),
                Replay = replay,
            };
            RenderModels.Add(renderItem);

            var progress = new Progress<int>(p =>
            {
                renderItem.Progress = p;
            });

            var (success, msg) = await renderService.RenderAsync(replay, progress);
            if (!success)
            {
                renderItem.IsFailed = true;
                renderItem.ErrorMessage = msg;
                return;
            }
            renderItem.Finished = true;
        }
    }
}
