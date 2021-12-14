using OsuDb.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class OsuDbService
    {
        public OsuDbService(OsuDbReader reader, Config config)
        {
            this.reader = reader;
            this.config = config;
        }

        private readonly OsuDbReader reader;
        private readonly Config config;

        public Core.Data.OsuScoresDb ScoreDb { get; private set; } = null!;
        public Core.Data.OsuBeatmapDb OsuBeatmapDb { get; private set; } = null!;

        public async Task RefreshScoreDbAsync()
        {
            await Task.Run(() =>
            {
                ScoreDb = reader.ReadScores(config.OsuScoreDbPath);
            }).ConfigureAwait(false);
        }

        public async Task RefreshBeatmapDbAsync()
        {
            OsuBeatmapDb = await reader.ReadBeatmapsAsync(config.OsuBeatmapDbPath).ConfigureAwait(false);
        }
    }
}
