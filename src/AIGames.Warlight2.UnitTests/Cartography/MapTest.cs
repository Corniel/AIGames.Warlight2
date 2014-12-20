using AIGames.Warlight2.Cartography;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.UnitTests.Cartography
{
    [TestFixture]
    public class MapTest
    {
        [Test]
        public void Contains_Region12_AreEqual()
        {
            var map = UnitTestMap.Init();
            var region = map[12];

            var act = map.Contains(region);
            var exp = true;

            Assert.AreEqual(exp, act);
        }

        [Test]
        public void PickableStartRegions_Select_AreEqual()
        {
            var map = UnitTestMap.InitSmall();

            var act = map.PickableStartRegions.Select(region => region.Id).ToArray();
            var exp = new int[] { 1, 2, 3, 4, 7, 8, 9, 11 };

           CollectionAssert.AreEqual(exp, act);
        }

        [Test]
        public void Select_3Items_AreEqual()
        {
            var map = UnitTestMap.InitSmall();

            var act = map.Select(1, 2, 4).ToList();
            var exp = new List<Region>()
            {
                map[1],
                map[2],
                map[4],
            };

            CollectionAssert.AreEqual(exp, act);
        }
    }
}
