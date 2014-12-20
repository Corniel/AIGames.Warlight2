using AIGames.Warlight2.Game;
using NUnit.Framework;

namespace AIGames.Warlight2.UnitTests.Game
{
	[TestFixture]
	public class PlayerTypeTest
	{
		[Test]
		public void Other_Player1_Player2()
		{
			Assert.AreEqual(PlayerType.player2, PlayerType.player1.Other());
		}
		[Test]
		public void Other_Player2_Player1()
		{
			Assert.AreEqual(PlayerType.player1, PlayerType.player2.Other());
		}
		[Test]
		public void Other_Player2_None_Player2_None()
		{
			Assert.AreEqual(PlayerType.neutral, PlayerType.neutral.Other());
		}
	}
}
