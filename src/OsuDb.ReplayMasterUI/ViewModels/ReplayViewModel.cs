using OsuDb.ReplayMasterUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.ViewModels
{
    internal class ReplayViewModel
    {
        public ReplayViewModel(OsuDbService dbService)
        {
            this.osuDbService = dbService;
        }

        

        private readonly OsuDbService osuDbService;
    }
}
