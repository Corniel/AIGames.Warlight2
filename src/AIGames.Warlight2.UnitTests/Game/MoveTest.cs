using AIGames.Warlight2.Game;
using NUnit.Framework;

namespace AIGames.Warlight2.UnitTests.Game
{
	[TestFixture]
	public class MoveTest
	{
		[Test]
		public void CreateSet_Params_areEqual()
		{
			var move = Move.CreateSet(PlayerType.player2, 42, 8);
			var act = move.DebuggerDisplay;
			var exp = "Set(2): 42 (8)";

			Assert.AreEqual(exp, act);
		}
		[Test]
		public void CreateSet_FromRegion_areEqual()
		{
			var map = UnitTestMap.Init();

			var move = Move.CreateSet(PlayerType.player2, map[42], 8);
			var act = move.DebuggerDisplay;
			var exp = "Set(2): 42 (8)";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void CreateSelect_Params_areEqual()
		{
			var move = Move.CreateSelect(PlayerType.player1, 17);
			var act = move.DebuggerDisplay;
			var exp = "Select(1): 17";

			Assert.AreEqual(exp, act);
		}
		[Test]
		public void CreateSelect_FromRegion_areEqual()
		{
			var map = UnitTestMap.Init();

			var move = Move.CreateSelect(PlayerType.player2, map[17]);
			var act = move.DebuggerDisplay;
			var exp = "Select(2): 17";

			Assert.AreEqual(exp, act);
		}
		
		[Test]
		public void CreateStack_Params_areEqual()
		{
			var move = Move.CreateStack(PlayerType.player1, 17, 5);
			var act = move.DebuggerDisplay;
			var exp = "Stack(1): 17 (5)";

			Assert.AreEqual(exp, act);
		}
		[Test]
		public void CreateStack_FromRegion_areEqual()
		{
			var map = UnitTestMap.Init();

			var move = Move.CreateStack(PlayerType.player1, map[17], 5);
			var act = move.DebuggerDisplay;
			var exp = "Stack(1): 17 (5)";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void CreateTransfer_Params_areEqual()
		{
			var move = Move.CreateTransfer(PlayerType.player1, 17, 42, 18);
			var act = move.DebuggerDisplay;
			var exp = "A/T(1): 17=>42 (18)";

			Assert.AreEqual(exp, act);
		}
		[Test]
		public void CreateTransfer_FromRegions_areEqual()
		{
			var map = UnitTestMap.Init();

			var move = Move.CreateTransfer(PlayerType.player1, map[17], map[42], 18);
			var act = move.DebuggerDisplay;
			var exp = "A/T(1): 17=>42 (18)";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void NoMoves_Params_areEqual()
		{
			var move = Move.NoMove;
			var act = move.DebuggerDisplay;
			var exp = "NoMove";

			Assert.AreEqual(exp, act);
		}
	}
}
