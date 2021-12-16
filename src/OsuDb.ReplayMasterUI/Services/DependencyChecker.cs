using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class DependencyChecker
    {
        public DependencyChecker(Config config)
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
                return Process.Start(p)!.StandardError.ReadToEnd().Contains("java version");
            }
        }

        public bool IsEncoderExsists => File.Exists(config.EncoderPath);

        public bool IsReplayMasterExsists => File.Exists(config.ReplayMasterPath);
    
        private Config config;
    }
}
