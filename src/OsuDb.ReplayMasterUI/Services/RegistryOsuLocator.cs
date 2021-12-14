using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace OsuDb.ReplayMasterUI.Services
{
    public class RegistryOsuLocator : IOsuLocator
    {
        public DirectoryInfo? GetOsuRootDirectory()
        {
            using var registry = Registry.ClassesRoot.OpenSubKey(@"osu\shell\open\command");
            if (registry is null) return null;

            var value = registry.GetValue(string.Empty)?.ToString()!;

            var osuExePathMatch = Regex.Match(value, @"(.:.*?osu!.exe)");
            if (!osuExePathMatch.Success) return null;

            var file = new FileInfo(osuExePathMatch.Groups[1].Value);
            return file.Directory;
        }
    }
}
