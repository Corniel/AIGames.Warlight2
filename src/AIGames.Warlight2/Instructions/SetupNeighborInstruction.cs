using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Instructions
{
	public class SetupNeighborInstruction : Instruction
	{
		/// <summary>Constructor.</summary>
		public SetupNeighborInstruction(IEnumerable<NeighborInfo> neighbors)
			: base(PlayerType.neutral)
		{
			Guard.NotNull(neighbors, "neighbors");
			this.Neighbors = neighbors.ToArray();
		}

		/// <summary>Gets the super regions.</summary>
		public NeighborInfo[] Neighbors { get; private set; }


		/// <summary>Represents the instruction as System.String.</summary>
		public override string ToString()
		{
			return string.Format("setup_map neighbors {0}", String.Join(" ", Neighbors.AsEnumerable()));
		}

		public static SetupNeighborInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");
			
			if (tokens.Length % 2 != 0 || tokens.Length < 4)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[0] != "setup_map" || tokens[1] != "neighbors")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var list = new List<NeighborInfo>();
			for (int i = 2; i < tokens.Length; i += 2)
			{
				list.Add(NeighborInfo.Create(tokens[i], tokens[i + 1]));
			}

			return new SetupNeighborInstruction(list);
		}
	}
}
