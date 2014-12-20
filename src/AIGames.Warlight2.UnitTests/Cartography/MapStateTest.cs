using AIGames.Warlight2.Cartography;
using AIGames.Warlight2.Debugging;
using AIGames.Warlight2.Game;
using NUnit.Framework;
using System;
using System.Text;

namespace AIGames.Warlight2.UnitTests.Cartography
{
	[TestFixture]
	public class MapStateTest
	{
		public Map TestMap { get { return UnitTestMap.InitSmall(); } }

		[Test]
		public void SetRoundSubRoundAndPlayerToMove_Round0PlaceArmiesPlayer1_AreEqual()
		{
			var state = new MapState(1);
			state.SetRoundSubRoundAndPlayerToMove(0, SubRoundType.PlaceArmies, PlayerType.player1);

			Assert.AreEqual(0, state.Round);
			Assert.AreEqual(SubRoundType.PlaceArmies, state.SubRound);
			Assert.AreEqual(PlayerType.player1, state.PlayerToMove);
		}

		[Test]
		public void SetRoundSubRoundAndPlayerToMove_Round0PlaceArmiesPlayer2_AreEqual()
		{
			var state = new MapState(1);
			state.SetRoundSubRoundAndPlayerToMove(0, SubRoundType.PlaceArmies, PlayerType.player2);

			Assert.AreEqual(0, state.Round);
			Assert.AreEqual(SubRoundType.PlaceArmies, state.SubRound);
			Assert.AreEqual(PlayerType.player2, state.PlayerToMove);
		}

		[Test]
		public void SetRoundSubRoundAndPlayerToMove_Round2AttackTransferPlayer2_AreEqual()
		{
			var state = new MapState(1);
			state.SetRoundSubRoundAndPlayerToMove(2, SubRoundType.AttackTransfer, PlayerType.player2);
			
			Assert.AreEqual(2, state.Round);
			Assert.AreEqual(SubRoundType.AttackTransfer, state.SubRound);
			Assert.AreEqual(PlayerType.player2, state.PlayerToMove);
		}
		[Test]
		public void SetRoundSubRoundAndPlayerToMove_Round3PlaceArmiesPlayer1_AreEqual()
		{
			var state = new MapState(1);
			state.SetRoundSubRoundAndPlayerToMove(3, SubRoundType.PlaceArmies, PlayerType.player1);

			Assert.AreEqual(3, state.Round);
			Assert.AreEqual(SubRoundType.PlaceArmies, state.SubRound);
			Assert.AreEqual(PlayerType.player1, state.PlayerToMove);
		}

		[Test]
		public void Count_None_AreEqual()
		{
			var exp = 17; 
			var state = new MapState(exp);
			var act = state.Count;

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Armies_Region_AreEqual()
		{
			var map = UnitTestMap.InitSmall();

			var state = new MapState(map.Count);
			state.Set(1, (ushort)PlayerType.player1, 13);

			var act = state.Armies(map[1]);
			var exp = 13;

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Owner_Region_AreEqual()
		{
			var map = UnitTestMap.InitSmall();

			var state = new MapState(map.Count);
			state.Set(1, 2, 13);

			var act = state.Owner(map[1]);
			var exp = PlayerType.player2;

			Assert.AreEqual(exp, act);
		}
#if !DEBUG
		[Test, ExpectedException(typeof(InvalidCastException))]
		public void Equals_12_ThrowsInvalidCastException()
		{
			var state = new MapState(1);
			state.Set(1, (ushort)PlayerType.player1, 13);
			state.Equals(12);
		}
#endif

		[Test]
		public void Equals_Other_IsFalse()
		{
			var act = new MapState(1);
			act.Set(1, (ushort)PlayerType.player1, 13);

			var other = new MapState(1);
			other.Set(1, (ushort)PlayerType.player1, 14);

			Assert.IsFalse(act.Equals(other));
		}

		[Test]
		public void Copy_Region_AreEqualButNotIdentical()
		{
			var source = new MapState(1);
			source.Set(1, (ushort)PlayerType.player1, 13);

			var target = source.Copy();

			Assert.AreEqual(source, target);
			Assert.AreNotSame(source, target);
		}

		[Test]
		public void UpdateRegion_Range_AreEqual()
		{
			for (int armies = 1; armies < 0x0fff; armies++)
			{
				for (ushort owner = 0; owner <= 2; owner++)
				{
					var mapstate = new MapState(1);
					PlayerType player = (PlayerType)owner;
					mapstate.Set(1, owner, (ushort)armies);

					Assert.AreEqual(player, mapstate.Owner(1), "Owner, Mapstate(1, {0}, {1})", player, armies);
					Assert.AreEqual(armies, mapstate.Armies(1), "Armies, Mapstate(1, {0}, {1})", player, armies);
				}
			}
		}

		[Test]
		public void Apply_Set18_AreEqual()
		{
			var mapstate = new MapState(1);
			mapstate.Set(1, (ushort)PlayerType.player1, 13);
			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner, Before");
			Assert.AreEqual(13, mapstate.Armies(1), "Armies, Before");

			mapstate.Apply(Move.CreateSet(PlayerType.player1, 1, 18), this.TestMap);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner, After");
			Assert.AreEqual(18, mapstate.Armies(1), "Armies, After");
		}

		[Test]
		public void Apply_Stack18_AreEqual()
		{
			var mapstate = new MapState(1);
			mapstate.Set(1, (ushort)PlayerType.player1, 13);
			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner, Before");
			Assert.AreEqual(13, mapstate.Armies(1), "Armies, Before");

			mapstate.Apply(Move.CreateStack(PlayerType.player1, 1, 18), this.TestMap);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner, After");
			Assert.AreEqual(31, mapstate.Armies(1), "Armies, After");
		}

		[Test]
		public void Apply_Select_AreEqual()
		{
			var mapstate = new MapState(1);
			mapstate.Set(1, (ushort)PlayerType.player1, 13);
			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner, Before");
			Assert.AreEqual(13, mapstate.Armies(1), "Armies, Before");

			mapstate.Apply(Move.CreateSelect(PlayerType.player1, 1), this.TestMap);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner, After");
			Assert.AreEqual(2, mapstate.Armies(1), "Armies, After");
		}

		[Test]
		public void Apply_Attack20vs10_AreEqual()
		{
			var mapstate = new MapState(2);
			mapstate.Set(1, (ushort)PlayerType.player1, 21);
			mapstate.Set(2, (ushort)PlayerType.player2, 10);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner[1], Before");
			Assert.AreEqual(21, mapstate.Armies(1), "Armies[1], Before");

			Assert.AreEqual(PlayerType.player2, mapstate.Owner(2), "Owner[2], Before");
			Assert.AreEqual(10, mapstate.Armies(2), "Armies[2], Before");

			mapstate.Apply(Move.CreateTransfer(PlayerType.player1, 1, 2, 20), this.TestMap);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner[2], After");
			Assert.AreEqual(1, mapstate.Armies(1), "Armies[1], After");

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(2), "Owner[2], After");
			Assert.AreEqual(13, mapstate.Armies(2), "Armies[2], After");
		}

		[Test]
		public void Apply_Attack10vs20_AreEqual()
		{
			var mapstate = new MapState(2);
			mapstate.Set(1, (ushort)PlayerType.player1, 11);
			mapstate.Set(2, (ushort)PlayerType.player2, 20);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner[1], Before");
			Assert.AreEqual(11, mapstate.Armies(1), "Armies[1], Before");

			Assert.AreEqual(PlayerType.player2, mapstate.Owner(2), "Owner[2], Before");
			Assert.AreEqual(20, mapstate.Armies(2), "Armies[2], Before");

			mapstate.Apply(Move.CreateTransfer(PlayerType.player1, 1, 2, 10), this.TestMap);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner[2], After");
			Assert.AreEqual(1, mapstate.Armies(1), "Armies[1], After");

			Assert.AreEqual(PlayerType.player2, mapstate.Owner(2), "Owner[2], After");
			Assert.AreEqual(14, mapstate.Armies(2), "Armies[2], After");
		}

		[Test]
		public void Apply_Transform7_AreEqual()
		{
			var mapstate = new MapState(2);
			mapstate.Set(1, (ushort)PlayerType.player1, 13);
			mapstate.Set(2, (ushort)PlayerType.player1, 03);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner[1], Before");
			Assert.AreEqual(13, mapstate.Armies(1), "Armies[1], Before");
			
			Assert.AreEqual(PlayerType.player1, mapstate.Owner(2), "Owner[2], Before");
			Assert.AreEqual(3, mapstate.Armies(2), "Armies[2], Before");
			
			mapstate.Apply(Move.CreateTransfer(PlayerType.player1, 1, 2, 7), this.TestMap);

			Assert.AreEqual(PlayerType.player1, mapstate.Owner(1), "Owner[2], After");
			Assert.AreEqual(06, mapstate.Armies(1), "Armies[1], After");
			
			Assert.AreEqual(PlayerType.player1, mapstate.Owner(2), "Owner[2], After");
			Assert.AreEqual(10, mapstate.Armies(2), "Armies[2], After");
		}
			  
		[Test]
		public void GetHashCode_DefaultValue_AreEqual()
		{
			var state = new MapState(1);

			var act = state.GetHashCode();
			var exp = 0;
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void GetHashCode_Transforms_AreEqual()
		{
			var exp = 16778040;

			var state0 = new MapState(2);
			state0.Set(1, 2, 13);
			state0.Set(2, 1, 53);
			state0.NextStep(2);
			state0.NextStep(2);

			var act0 = state0.GetHashCode();
			Assert.AreEqual(exp, act0, "hash act0");

			var state1 = new MapState(2);
			state1.Set(1, 2, 13);
			state1.Set(2, 1, 53);
			state1.NextStep(2);
			state1.Set(2, 0, 2);
			state1.Set(2, 2, 2);
			state1.Apply(Move.CreateStack(PlayerType.player2, 2, 13), this.TestMap);
			state1.Set(2, 1, 53);
			state1.Set(1, 2, 10);
			state1.Apply(Move.CreateStack(PlayerType.player1, 1, 3), this.TestMap);
			state1.NextStep(2);

			var act1 = state1.GetHashCode();
#if DEBUG
			DebugRegions debug0 = state0.ToDebug();
			DebugRegions debug1 = state1.ToDebug();
#endif
			Assert.AreEqual(state0.Owner(1), state1.Owner(1), "Owner[1]");
			Assert.AreEqual(state0.Armies(1), state1.Armies(1), "Armies[1]");

			Assert.AreEqual(state0.Owner(2), state1.Owner(2), "Owner[2]");
			Assert.AreEqual(state0.Armies(2), state1.Armies(2), "Armies[2]");

			//Assert.AreEqual(state0.PlayerToMove, state1.PlayerToMove, "PlayerToMove");
			Assert.AreEqual(state0.SubRound, state1.SubRound, "SubRound");


			Assert.AreEqual(exp, act1, "hash act1");
		}

		[Test]
		public void NextStep_13Times_AreEqual()
		{
			var state = new MapState(1);

			var sb = new StringBuilder();

			var round = 0;
			var sub = SubRoundType.PlaceArmies;
			for (int i = 0; i < 13; i++)
			{
				sb.AppendFormat("{0:00}.{1}.{2}.D{3:00}", state.Round, state.SubRound.ToString()[0], state.PlayerToMove, state.Depth);
				sb.AppendLine();
				state.NextStep(2);

				if (round != state.Round)
				{
					sb.AppendLine("================");
				}
				else if (sub != state.SubRound)
				{
					sb.AppendLine("----------------");
				}

				round = state.Round;
				sub = state.SubRound;
			}
			var act = sb.ToString();
			var exp = @"00.P.player2.D00
00.P.player1.D01
================
01.P.player1.D04
01.P.player2.D05
----------------
01.A.player1.D06
01.A.player2.D07
================
02.P.player2.D08
02.P.player1.D09
----------------
02.A.player2.D10
02.A.player1.D11
================
03.P.player1.D12
03.P.player2.D13
----------------
03.A.player1.D14
";
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void DebuggerDisplay_None_AreEqual()
		{
			var state = new MapState(1);
			state.Set(1, (ushort)PlayerType.player1, 13);

			var act = state.DebuggerDisplay;
			var exp = "MapState[0.PlaceArmies] player2, Regions: 1";

			Assert.AreEqual(exp, act);
		}
	}
}
