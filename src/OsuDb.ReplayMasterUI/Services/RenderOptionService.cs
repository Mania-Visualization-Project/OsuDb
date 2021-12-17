using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class RenderOptionService
    {
        public RenderOptionService(Config config)
        {
            this.config = config;
            if (File.Exists(config.ConfigsJsonPath))
            {
                using var file = File.OpenRead(config.ConfigsJsonPath);
                RenderOptions = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, RenderOptions>>(file)
                    ?? throw new FieldAccessException();
            }
            else
            {
                RenderOptions = new();
                RenderOptions.Add("default", DefaultOptions);
            }
        }

        public Dictionary<string, RenderOptions> RenderOptions { get; set; }

        public async Task SaveOptions()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(RenderOptions);
            using var file = File.OpenWrite(config.ConfigsJsonPath);
            using var writer = new StreamWriter(file);
            await writer.WriteAsync(json);
            await writer.FlushAsync();
            writer.Close();
        }

        public string GetRenderOptionsFile(string name = "default")
        {
            var option = RenderOptions.ContainsKey(name) ? RenderOptions[name] : DefaultOptions;
            option.OutputDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var yamlSerializer = new SharpYaml.Serialization.Serializer();
            var yaml = yamlSerializer.Serialize(option, null);

            yaml = '#' + yaml[1..];

            var outputFile = Path.GetTempFileName();
            using var file = File.OpenWrite(outputFile);
            using var writer = new StreamWriter(file);
            writer.Write(yaml);
            writer.Flush();
            return outputFile;
        }

        private readonly Config config;

        private static RenderOptions DefaultOptions => new()
        {
            Speed = 20,
            FramePerSecond = 60,
            Width = 540,
            Height = 960,
            ActionHeight = 7,
            BlockHeight = 40,
            Stroke = 3,
            LongNoteColor = "646464",
            MissColor = "FF0000",
            JudgementColors = new string[] { "FFFFFF", "FFD237", "79D020", "1E68C5", "E1349B" }
        };
    }
}
