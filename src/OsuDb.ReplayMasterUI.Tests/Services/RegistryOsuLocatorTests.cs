using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsuDb.ReplayMasterUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDb.ReplayMasterUI.Services.Tests
{
    [TestClass()]
    public class RegistryOsuLocatorTests
    {
        [TestMethod()]
        public void GetOsuRootDirectoryTest()
        {
            var locator = new RegistryOsuLocator();
            var dir = locator.GetOsuRootDirectory();
            Assert.IsNotNull(dir);
        }
    }
}