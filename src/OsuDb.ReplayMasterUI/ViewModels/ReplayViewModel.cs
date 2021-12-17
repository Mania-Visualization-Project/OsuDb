using OsuDb.Core.Data;
using OsuDb.ReplayMasterUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class ReplayViewModel
    {
        public ReplayViewModel(OsuDbService dbService)
        {
            this.osuDbService = dbService;
        }

        public ObservableCollection<OsuReplayModel> Replays => replays ??= new ObservableCollection<OsuReplayModel>();

        public void Filter(Func<IEnumerable<OsuReplayModel>, IEnumerable<OsuReplayModel>> filter)
        {
            var filtered = filter.Invoke(GetAllreplaysFromCache());
            Replays.Clear();
            foreach (var replay in filtered.OrderByDescending(x => x.PlayTime))
            {
                Replays.Add(replay);
            }
        }

        public async Task RefreshAsync(IProgress<(int,int)> progress)
        {
            if (osuDbService.OsuBeatmapDb is null)
                await osuDbService.RefreshBeatmapDbAsync(progress).ConfigureAwait(false);
            await osuDbService.RefreshScoreDbAsync(progress).ConfigureAwait(false);

            Replays.Clear();
            foreach (var replay in GetAllreplaysFromCache().OrderByDescending(x => x.PlayTime))
            {
                Replays.Add(replay);
            }
        }

        private readonly OsuDbService osuDbService;
        private ObservableCollection<OsuReplayModel> replays = null!;

        private IEnumerable<OsuReplayModel> GetAllreplaysFromCache()
        {
            foreach (var score in osuDbService.ScoreDb.Scores.OrderByDescending(x => x.PlayTime))
            {
                var mapExsists = osuDbService.OsuBeatmapDb!.BeatmapHashDic.TryGetValue(score.BeatmapMd5, out var beatmap);
                if (!mapExsists) continue;
                yield return new OsuReplayModel
                {
                    BeatmapMd5 = score.BeatmapMd5,
                    Combo = score.Combo,
                    Count100 = score.Count100,
                    Count200 = score.Count200,
                    Count300 = score.Count300,
                    Count300Plus = score.Count300Plus,
                    Count50 = score.Count50,
                    CountMiss = score.CountMiss,
                    IsFullCombo = score.IsFullCombo,
                    PlayTime = score.PlayTime,
                    Score = (int)score.Score,
                    Player = score.PlayerName,
                    Title = $"{beatmap!.Artist} - {beatmap.Title} [{beatmap.Difficulty}]",
                    AudioFileName = beatmap.AudioFileName,
                    BeatmapFileName = beatmap.BeatmapFileName,
                    FolderName = beatmap.FolderName,
                    Mode = beatmap.GameMode
                };
            }
        }
    }
}
