using OsuDb.Core.Data;
using OsuDb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.Core
{
    public class OsuDbReader
    {
        public static OsuScoresDb ReadScores(string dbFilePath)
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

            // read all collections
            while (collectionCount --> 0)
            {
                var md5 = reader.ReadOsuString();
                var count = reader.ReadInt32();
                var records = ReadRecords(reader, count);
                dic.Add(md5, records);
            }

            reader.Close();
            file.Close();

            return result;
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
