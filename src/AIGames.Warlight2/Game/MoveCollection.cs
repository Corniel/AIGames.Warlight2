using AIGames.Warlight2.Cartography;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AIGames.Warlight2.Game
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class MoveCollection : List<Move>
	{
		public MoveCollection Copy()
		{
			var copy = new MoveCollection();
			copy.AddRange(this);
			return copy;
		}

		public static MoveCollection CreateStack(PlayerType player, int[] stacks)
		{
			var moves = new MoveCollection();
			for (int id = 1; id < stacks.Length; id++)
			{
				if (stacks[id] > 0)
				{
					moves.Add(Move.CreateStack(player, id, stacks[id]));
				}
			}
			return moves;
		}

		public static MoveCollection CreateNoMoves()
		{
			return new MoveCollection();
		}
		public static MoveCollection CreateStack(PlayerType player, Region region, int stack)
		{
			return new MoveCollection()
			{
				Move.CreateStack(player, region.Id, stack)
			};
		}

		[ExcludeFromCodeCoverage, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string DebuggerDisplay
		{
			get
			{
				if (this.Count == 0) { return "No moves"; }
				return string.Join(", ", this.Select(item => item.DebuggerDisplay));
			}
		}
	}
}
