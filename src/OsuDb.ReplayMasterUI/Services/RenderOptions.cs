using SharpYaml.Serialization;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class RenderOptions
    {
        [YamlMember("speed")]
        public int Speed { get; set; }

        [YamlMember("framePerSecond")]
        public int FramePerSecond { get; set; }

        [YamlMember("width")]
        public int Width { get; set; }

        [YamlMember("height")]
        public int Height { get; set; }

        [YamlMember("actionHeight")]
        public int ActionHeight { get; set; }

        [YamlMember("blockHeight")]
        public int BlockHeight { get; set; }

        [YamlMember("stroke")]
        public int Stroke { get; set; }

        [YamlMember("judgementColor")]
        public string[] JudgementColors { get; set; } = null!;

        [YamlMember("longNoteColor")]
        public string LongNoteColor { get; set; } = null!;

        [YamlMember("missColor")]
        public string MissColor { get; set; } = null!;

        [YamlMember("outputDir")]
        public string OutputDir { get; set; } = null!;

        [YamlMember("exportJudgementResults")]
        public bool ExportJudgementResult { get; set; } = false;

        [YamlMember("codec")]
        public string Codec { get; set; } = "libx264";

        [YamlMember("debug")]
        public bool Debug { get; set; } = false;

        [YamlMember("desktop")]
        public bool Desktop { get; set; } = true;
    }
}
