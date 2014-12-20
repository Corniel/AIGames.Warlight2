#if DEBUG

using AIGames.Warlight2.Debugging;
using AIGames.Warlight2.Game;
using NUnit.Framework;

namespace AIGames.Warlight2.UnitTests.Debugging
{
	[TestFixture]
	public class DebugRegionTest
	{
		[Test]
		public void ToString_Region_AreEqual()
		{
			var debug = new DebugRegion(8, PlayerType.player1, 8);

			var act = debug.ToString();
			var exp = "ID: 8, Owner: player1, Armies: 8";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void ToString_MovedRegion_AreEqual()
		{
			var debug = new DebugRegion(1, PlayerType.player2, 3);
			
			var act = debug.ToString();
			var exp = "ID: 1, Owner: player2, Armies: 3";

			Assert.AreEqual(exp, act);
		}
	}
}
#endif
