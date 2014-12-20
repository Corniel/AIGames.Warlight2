using AIGames.Warlight2.Cartography;
using AIGames.Warlight2.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.UnitTests.Cartography
{
	[TestFixture]
	public class RegionStateTest
	{
		public static readonly RegionState TestValue = RegionState.Create(17, PlayerType.player1);

		[Test]
		public void Create_P1Armies17_AreEqual()
		{
			var act = RegionState.Create(17, PlayerType.player1);

			var expOwner = PlayerType.player1;
			var expArmies = 17;

			Assert.AreEqual(expOwner, act.Owner, "Owner");
			Assert.AreEqual(expArmies, act.Armies, "Armies");
		}

		[Test]
		public void GetHashCode_TestValue_AreEqual()
		{
			var act = TestValue.GetHashCode();
			var exp = 69;

			Assert.AreEqual(exp, act);
		}
	}
}
