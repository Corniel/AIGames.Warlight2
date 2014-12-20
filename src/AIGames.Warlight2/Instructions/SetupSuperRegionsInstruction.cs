using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Instructions
{
	public class SetupSuperRegionsInstruction : Instruction
	{
		/// <summary>Constructor.</summary>
		public SetupSuperRegionsInstruction(IEnumerable<SuperRegionInfo> superregions)
			: base(PlayerType.neutral)
		{
			Guard.NotNull(superregions, "superregions");
			this.SuperRegions = superregions.ToArray();
		}

		/// <summary>Gets the super regions.</summary>
		public SuperRegionInfo[] SuperRegions { get; private set; }


		/// <summary>Represents the instruction as System.String.</summary>
		public override string ToString()
		{
			return string.Format("setup_map super_regions {0}", String.Join(" ", SuperRegions.AsEnumerable()));
		}

		public static SetupSuperRegionsInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");
			
			if (tokens.Length % 2 != 0 || tokens.Length < 4)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[0] != "setup_map" || tokens[1] != "super_regions")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var list = new List<SuperRegionInfo>();
			for (int i = 2; i < tokens.Length; i += 2)
			{
				list.Add(SuperRegionInfo.Create(tokens[i], tokens[i + 1]));
			}

			return new SetupSuperRegionsInstruction(list);
		}
	}
}
