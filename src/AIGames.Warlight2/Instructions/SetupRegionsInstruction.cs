using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Instructions
{
	public class SetupRegionsInstruction : Instruction
	{
		/// <summary>Constructor.</summary>
		public SetupRegionsInstruction(IEnumerable<RegionInfo> regions)
			: base(PlayerType.neutral)
		{
			Guard.NotNull(regions, "regions");
			this.Regions = regions.ToArray();
		}

		/// <summary>Gets the super regions.</summary>
		public RegionInfo[] Regions { get; private set; }


		/// <summary>Represents the instruction as System.String.</summary>
		public override string ToString()
		{
			return string.Format("setup_map regions {0}", String.Join(" ", Regions.AsEnumerable()));
		}

		public static SetupRegionsInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");
			
			if (tokens.Length % 2 != 0 || tokens.Length < 4)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[0] != "setup_map" || tokens[1] != "regions")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var list = new List<RegionInfo>();
			for (int i = 2; i < tokens.Length; i += 2)
			{
				list.Add(RegionInfo.Create(tokens[i], tokens[i + 1]));
			}

			return new SetupRegionsInstruction(list);
		}
	}
}
