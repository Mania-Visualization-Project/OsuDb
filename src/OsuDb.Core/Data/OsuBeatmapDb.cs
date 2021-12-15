using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.Core.Data
{
    public class OsuBeatmapDb
    {
        public uint Version { get; set; }
        public IEnumerable<OsuBeatmap> Beatmaps { get; set; } = null!;
        public Dictionary<string, OsuBeatmap> BeatmapHashDic => beatmapHashDic ??= ParseDictionary(Beatmaps);

        private Dictionary<string, OsuBeatmap> beatmapHashDic = null!;

        private Dictionary<string, OsuBeatmap> ParseDictionary(IEnumerable<OsuBeatmap> beatmaps)
        {
            var result = new Dictionary<string, OsuBeatmap>();
            foreach (var beatmap in beatmaps)
            {
                if (result.ContainsKey(beatmap.Md5)) continue;
                result.Add(beatmap.Md5, beatmap);
            }
            return result;
        }
    }
}
