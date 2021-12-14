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
    }
}
