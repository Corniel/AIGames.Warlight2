using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GentleWare.Conquest.UnitTests.Cartography
{
    [TestClass]
    public class SuperRegionTest
    {
        [TestMethod]
        public void DebugToString_Australia_AreEqual()
        {
            var map = UnitTestMap.Init();
            var superregion = map.SuperRegions.Single(superRegion => superRegion.Id == 6);

            var act = superregion.DebugToString();
            var exp = "SuperRegion[6,2]: Australia, Regions: 4";

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void SelectByIndex_39Australia_AreEqual()
        {
            var map = UnitTestMap.Init();
            var superregion = map.SuperRegions.Single(superRegion => superRegion.Id == 6);

            var act = superregion[39];
            var exp = map[39];

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Contains_12_AreEqual()
        {
            var map = UnitTestMap.Init();
            var superregion = map.SuperRegions.Single(superRegion => superRegion.Id == 6);

            var act = superregion.Contains(map[12]);
            var exp = false;

            Assert.AreEqual(exp, act);
        }
    }
}
