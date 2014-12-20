using AIGames.Warlight2.Cartography;
using AIGames.Warlight2.Game;
using NUnit.Framework;
using System.Linq;

namespace AIGames.Warlight2.UnitTests.Cartography
{
	[TestFixture]
	public class RegionCollectionTest
	{
		[Test]
		public void GetBorders_Australia_AreEqual()
		{
			var map = UnitTestMap.Init();

			var superregion = map.SuperRegions.Single(superRegion => superRegion.Id == 6);

			var act = superregion.GetBorders().ToArray();
			var exp = new Region[] { map[39] };

			CollectionAssert.AreEqual(exp, act);
		}

		[Test]
		public void GetIncludingNeighbors_SouthAmerica_AreEqual()
		{
			var map = UnitTestMap.Init();

			var superregion = map.SuperRegions.Single(superRegion => superRegion.Id == 2);

			var act = superregion.GetIncludingNeighbors().ToArray();
			var exp = new Region[] { map[9], map[11], map[12], map[10], map[13], map[21] };

			CollectionAssert.AreEqual(exp, act);
		}

		[Test]
		public void GetNeighbors_Afrika_AreEqual()
		{
			var map = UnitTestMap.Init();

			var superregion = map.SuperRegions.Single(superRegion => superRegion.Id == 4);

			var act = superregion.GetNeighbors().ToArray();
			var exp = new Region[] { map[12], map[18], map[20], map[36] };

			CollectionAssert.AreEqual(exp, act);
		}

		[Test]
		public void GetRegionsForPlayer_MapInit_AreEqual()
		{
			var map = UnitTestMap.Init();
			var state = map.ToInitialRegionState();
			state.Set(03, (int)PlayerType.player1, 2);
			state.Set(14, (int)PlayerType.player1, 2);
			state.Set(21, (int)PlayerType.player1, 2);
			state.Set(16, (int)PlayerType.player2, 2);

			var player1s = map.GetRegionsForPlayer(PlayerType.player1, state).ToList();
			var player2s = map.GetRegionsForPlayer(PlayerType.player2, state).ToList();
			var neutrals = map.GetRegionsForPlayer(PlayerType.neutral, state).ToList();

			Assert.AreEqual(3, player1s.Count, "player1s.Count");
			Assert.AreEqual(1, player2s.Count, "player2s.Count");
			Assert.AreEqual(38, neutrals.Count, "neutrals.Count");
		}
	}
}
