using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal class DownloadService
    {
        public DownloadService()
        {
            client = new HttpClient();
        }

        private readonly HttpClient client;

        public async Task<string> DownloadMsOpenJdkAsync(IProgress<(long, long)> progress)
        {
            var result = await client.GetAsync("https://aka.ms/download-jdk/microsoft-jdk-11.0.13.8.1-windows-x64.msi",
                HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            result.EnsureSuccessStatusCode();

            var tempPath = Path.GetTempPath();
            var filePath = Path.Combine(tempPath, "msopenjdk.msi");
            using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192);

            var totalBytes = result.Content.Headers.ContentLength ?? throw new HttpRequestException("no content");
            var stream = await result.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var buffer = new byte[8192];

            var totalReadBytes = 0L;
            var reportBytes = 0L;
            while (totalReadBytes < totalBytes)
            {
                var readBytes = await stream.ReadAsync(buffer).ConfigureAwait(false);
                totalReadBytes += readBytes;
                reportBytes += readBytes;
                file.Write(buffer, 0, readBytes);
                if (reportBytes >= totalBytes / 100)
                {
                    progress.Report(new(totalReadBytes, totalBytes!));
                    reportBytes = 0;
                }  
            }

            file.Flush();
            file.Close();

            return filePath;
        }
    }
}
