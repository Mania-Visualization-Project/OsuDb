using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class DependencyEnsureService
    {
        public DependencyEnsureService(Config config)
        {
            this.config = config;
        }

        public bool IsJreInstalled
        {
            get
            {
                var p = new ProcessStartInfo
                {
                    FileName = "java.exe",
                    Arguments = "-version",
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                return Process.Start(p)!.StandardError.ReadToEnd().Contains("version");
            }
        }

        public bool EnsureDecoder()
        {
            if (!File.Exists(config.EncoderPath))
            {
                var ffmpegPath = Path.Combine(config.CurrentPath, "ffmpeg.exe");
                if (!File.Exists(ffmpegPath)) return false;
                File.Copy(ffmpegPath, config.EncoderPath);
            }
            return true;
        }

        public bool EnsureReplay()
        {
            if (!File.Exists(config.ReplayMasterPath))
            {
                var maniaReplayMasterPath = Path.Combine(config.CurrentPath, "ManiaReplayMaster.jar");
                if (!File.Exists(maniaReplayMasterPath)) return false;
                File.Copy(maniaReplayMasterPath, config.ReplayMasterPath);
            }
            return true;
        }
    
        private Config config;
    }
}
