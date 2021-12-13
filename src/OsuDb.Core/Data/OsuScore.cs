namespace OsuDb.Core.Data
{
    public class OsuScore
    {
        public byte GameMode { get; set; }

        public uint OsuVersion { get; set; }

        public string BeatmapMd5 { get; set; } = null!;

        public string PlayerName { get; set; } = null!;

        public string ReplayMd5 { get; set; } = null!;

        public ushort Count300 { get; set; }

        public ushort Count100 { get; set; }

        public ushort Count50 { get; set; }

        public ushort Count300Plus { get; set; }

        public ushort Count200 { get; set; }

        public ushort CountMiss { get; set; }

        public uint Score { get; set; }

        public ushort Combo { get; set; }

        public bool IsFullCombo { get; set; }

        public uint Mods { get; set; }

        public string Performance { get; set; } = string.Empty;

        public DateTime PlayTime { get; set; }

        public ulong Id { get; set; }

        public override string ToString()
        {
            return $"{PlayerName},{BeatmapMd5},{Score},{Count300Plus},{Count300},{Count200},{Count100},{Count50},{CountMiss}";
        }
    }
}
