#if DEBUG

using AIGames.Warlight2.Cartography;
using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AIGames.Warlight2.Debugging
{
	public class DebugRegion
	{
		public DebugRegion(int id, PlayerType owner, int armies)
		{
			this.Id = id;
			this.Owner = owner;
			this.Armies = armies;
		}

		public int Id { get; private set; }
		public PlayerType Owner { get; private set; }
		public int Armies { get; private set; }

		/// <summary>Represents the region as debug string.</summary>
		public override string ToString()
		{
			return String.Format("ID: {0}, Owner: {1}, Armies: {2}",
				this.Id,
				this.Owner,
				this.Armies);
		}
	}

	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This name is clear.")]
	public class DebugRegions : List<DebugRegion>
	{
		public DebugRegions(int round, SubRoundType subRound, PlayerType playerToMove)
		{
			this.Round = round;
			this.SubRound = subRound;
			this.PlayerToMove = playerToMove;
		}
		/// <summary>Gets the round.</summary>
		public int Round { get; private set; }

		/// <summary>Gets the sub round.</summary>
		public SubRoundType SubRound { get; private set; }

		/// <summary>Gets the player to move.</summary>
		public PlayerType PlayerToMove { get; private set; }

		public DebugRegion Get(int id)
		{
			return this.FirstOrDefault(item => item.Id == id);
		}
	}

	public static class MapStateExtensions
	{
		public static DebugRegions ToDebug(this MapState state, bool excludeNeutral2 = true)
		{
			var regions = new DebugRegions(state.Round, state.SubRound, state.PlayerToMove);

			for (int id = 1; id <= state.Count; id++)
			{
				var owner = state.Owner(id);
				var armies = state.Armies(id);
				if (!excludeNeutral2 || owner != PlayerType.neutral || armies != 2)
				{
					regions.Add(new DebugRegion(id, owner, armies));
				}
			}
			return regions;
		}

		public static string ToUnitTestSetup(this MapState state)
		{
			var sb = new StringBuilder();

			sb.AppendFormat("state.SetRoundSubRoundAndPlayerToMove({0}, SubRoundType.{1}, PlayerType.{2});",
				state.Round,
				state.SubRound,
				state.PlayerToMove)
				.AppendLine();

			var regions = state.ToDebug();

			foreach (var region in regions.OrderBy(r => r.Owner))
			{
				sb.AppendFormat("state.Set({0}, {1}, {2});",
					region.Id,
					(int)region.Owner,
					region.Armies)
					.AppendLine();
			}
			return sb.ToString();
		}
	}
}
#endif
