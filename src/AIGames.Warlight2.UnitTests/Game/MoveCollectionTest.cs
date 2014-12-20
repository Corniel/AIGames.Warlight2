using AIGames.Warlight2.Game;
using NUnit.Framework;

namespace AIGames.Warlight2.UnitTests.Game
{
	[TestFixture]
	public class MoveCollectionTest
	{
		[Test]
		public void DebuggerDisplay_EmptyMoveCollection_AreEqual()
		{
			var moves = MoveCollection.CreateNoMoves();

			var act = moves.DebuggerDisplay;
			var exp = "No moves";

			Assert.AreEqual(exp, act);
		}
	}
}
