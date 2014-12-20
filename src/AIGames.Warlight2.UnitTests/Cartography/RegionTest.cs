using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GentleWare.Conquest.UnitTests.Cartography
{
    [TestClass]
    public class RegionTest
    {
        [TestMethod]
        public void DebugToString_Alaska_AreEqual()
        {
            var map = UnitTestMap.Init();
            var region = map[1];

            var act = region.DebugToString();
            var exp = "Region[1,1]: Alaska*, Neighbors(3):  Northwest Territory, Alberta, Kamchatka";

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void DebugToString_EasternAustralia_AreEqual()
        {
            var map = UnitTestMap.Init();
            var region = map[42];

            var act = region.DebugToString();
            var exp = "Region[42,6]: Eastern Australia, Neighbors(2):  New Guinea, Western Australia";

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Map_EasternAustralia_IsNotNull()
        {
            var map = UnitTestMap.Init();
            var region = map[42];

            var parent = region.Map;

            Assert.IsNotNull(parent);
            Assert.AreEqual(map, parent);
        }
    }
}
