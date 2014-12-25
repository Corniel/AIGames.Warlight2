using AIGames.Warlight2.Cartography;
#if DEBUG
using AIGames.Warlight2.Debugging;
#endif
using AIGames.Warlight2.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AIGames.Warlight2.UnitTests
{
	public static class MapStateAssert
	{
		[DebuggerStepThrough]
		public static void AreEqual(int expRound, SubRoundType expSubRound, PlayerType expPlayerToMove, MapState act)
		{
			Assert.AreEqual(expRound, act.Round, "Round");
			Assert.AreEqual(expSubRound, act.SubRound, "SubRound");
			Assert.AreEqual(expPlayerToMove, act.PlayerToMove, "PlayerToMove");
		}
	}
#if DEBUG
	public static class RegionsAssert
	{
		[DebuggerStepThrough]
		public static void AreEqual(DebugRegions exp, DebugRegions act)
		{
			Assert.AreEqual(exp.Round, act.Round, "Round");
			Assert.AreEqual(exp.SubRound, act.SubRound, "SubRound");
			Assert.AreEqual(exp.PlayerToMove, act.PlayerToMove, "PlayerToMove");

			foreach (var e in exp)
			{
				var a = act.Get(e.Id);
				AreEqual(e.Owner, e.Armies, a);
			}
		}
		[DebuggerStepThrough]
		public static void AreEqual(PlayerType expOwner, int expArmies, DebugRegion act)
		{
			Assert.AreEqual(expOwner, act.Owner, "Owner[{0}]", act.Id);
			Assert.AreEqual(expArmies, act.Armies, "Armies[{0}]", act.Id);
		}
	}
#endif
}
