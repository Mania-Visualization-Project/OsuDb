using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services
{
    internal interface IOsuLocator
    {
        DirectoryInfo? GetOsuRootDirectory();
    }
}
