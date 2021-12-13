using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.Core.Data
{
    public class OsuScoresDb
    {
        public uint Version { get; set; }

        public Dictionary<string, IEnumerable<OsuScore>> ScoreCollections { get; set; } = null!;

        public IEnumerable<OsuScore> Scores => scores ??= ScoreCollections.Values.SelectMany(x => x).ToList();

        private List<OsuScore> scores = null!;
    }
}
