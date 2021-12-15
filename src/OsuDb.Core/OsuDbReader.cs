using OsuDb.Core.Data;
using OsuDb.Core.Extensions;

namespace OsuDb.Core
{
    public class OsuDbReader
    {
        public async Task<OsuScoresDb> ReadScores(string dbFilePath, IProgress<(int, int)> progress)
        {
            _ = File.Exists(dbFilePath) ? true : throw new FileNotFoundException("cant find db file");
            using var file = File.OpenRead(dbFilePath);
            using var reader = new BinaryReader(file);

            // read header
            var version = reader.ReadUInt32();
            var collectionCount = reader.ReadInt32();

            // allocate result dictionary
            var result = new OsuScoresDb() { Version = version, ScoreCollections = new(collectionCount) };
            var dic = result.ScoreCollections;

            // read all collections async
            await Task.Run(() =>
            {
                for (var i = 0; i < collectionCount; i++)
                {
                    var md5 = reader.ReadOsuString();
                    var count = reader.ReadInt32();
                    var records = ReadRecords(reader, count);
                    dic.Add(md5, records);
                    progress.Report((i, collectionCount));
                }
            });

            reader.Close();
            file.Close();

            return result;
        }

        public async Task<OsuBeatmapDb> ReadBeatmapsAsync(string dbFilePath, IProgress<(int,int)> progress)
        {
            _ = File.Exists(dbFilePath) ? true : throw new FileNotFoundException("cant find db file");
            using var file = File.OpenRead(dbFilePath);
            using var reader = new BinaryReader(file);

            // read header
            var version = reader.ReadUInt32();
            var fileCount = reader.ReadInt32();

            // skip useless head data
            _ = reader.ReadByte();
            _ = reader.ReadInt64();
            _ = reader.ReadOsuString();
            _ = reader.ReadInt32();

            // allocate result dictionary
            var result = new OsuBeatmapDb() { Version = version };
            var list = new List<OsuBeatmap>(fileCount);

            await Task.Run(() =>
            {
                list.AddRange(ReadBeatmaps(reader, fileCount, progress));
            }).ConfigureAwait(false);

            result.Beatmaps = list;
            return result;
        }

        private static IEnumerable<OsuBeatmap> ReadBeatmaps(BinaryReader reader, int count, IProgress<(int,int)> progress)
        {
            for(var t = 1; t <= count; t++)
            {
                var result = new OsuBeatmap();
                result.Artist = reader.ReadOsuString();
                // Artist Unicode
                _ = reader.ReadOsuString();
                result.Title = reader.ReadOsuString();
                // Title Unicode
                _ = reader.ReadOsuString();
                result.Creator = reader.ReadOsuString();
                result.Difficulty = reader.ReadOsuString();
                result.AudioFileName = reader.ReadOsuString();
                result.Md5 = reader.ReadOsuString();
                result.BeatmapFileName = reader.ReadOsuString();
                // Unknown
                _ = reader.ReadByte();
                // Object Counts
                _ = reader.ReadUInt16();
                _ = reader.ReadUInt16();
                _ = reader.ReadUInt16();
                // Date Modified
                _ = reader.ReadInt64();
                // Difficulties
                _ = reader.ReadSingle();
                _ = reader.ReadSingle();
                _ = reader.ReadSingle();
                _ = reader.ReadSingle();
                // Unknown
                _ = reader.ReadDouble();
                // Skip Unknown Segment
                for (var i = 0; i < 4; i++)
                {
                    var n = reader.ReadInt32();
                    if (n > 0) _ = reader.ReadBytes(n * 14);
                }
                // Length
                _ = reader.ReadInt32();
                // Unknown
                _ = reader.ReadInt32();
                _ = reader.ReadInt32();
                // Skip Unkown Segment
                var k = reader.ReadInt32();
                if (k > 0) _ = reader.ReadBytes(k * 17);
                // Beatmap Id
                _ = reader.ReadInt32();
                // Beatmap Set Id
                _ = reader.ReadInt32();
                // Unknown
                _ = reader.ReadInt32();
                // 4 Ranks
                _ = reader.ReadInt32();
                // User Offset
                _ = reader.ReadInt16();
                // Unknown
                _ = reader.ReadSingle();
                result.GameMode = (GameMode)reader.ReadByte();
                // Source
                _ = reader.ReadOsuString();
                // Tags
                _ = reader.ReadOsuString();
                // Unknown
                _ = reader.ReadUInt16();
                _ = reader.ReadOsuString();
                _ = reader.ReadByte();
                // Last Played Time
                _ = reader.ReadInt64();
                _ = reader.ReadByte();
                result.FolderName = reader.ReadOsuString();
                // Skip Else 18 bytes
                _ = reader.ReadBytes(18);
                progress.Report((t, count));
                yield return result;
            }
        }

        private static IEnumerable<OsuScore> ReadRecords(BinaryReader reader, int count)
        {
            var result = new OsuScore[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = ReadRecord(reader);
            }

            return result;
        }

        private static OsuScore ReadRecord(BinaryReader reader)
        {
            var result = new OsuScore
            {
                GameMode = reader.ReadByte(),
                OsuVersion = reader.ReadUInt32(),
                BeatmapMd5 = reader.ReadOsuString(),
                PlayerName = reader.ReadOsuString(),
                ReplayMd5 = reader.ReadOsuString(),
                Count300 = reader.ReadUInt16(),
                Count100 = reader.ReadUInt16(),
                Count50 = reader.ReadUInt16(),
                Count300Plus = reader.ReadUInt16(),
                Count200 = reader.ReadUInt16(),
                CountMiss = reader.ReadUInt16(),
                Score = reader.ReadUInt32(),
                Combo = reader.ReadUInt16(),
                IsFullCombo = reader.ReadBoolean(),
                Mods = reader.ReadUInt32(),
                Performance = reader.ReadOsuString(),
                PlayTime = DateTime.FromBinary(reader.ReadInt64()).ToLocalTime(),
            };
            _ = reader.ReadInt32();
            result.Id = reader.ReadUInt64();

            return result;
        }
    }
}
