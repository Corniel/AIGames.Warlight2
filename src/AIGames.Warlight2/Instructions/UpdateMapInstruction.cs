using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Instructions
{
	/// <summary>Represents an update map instruction.</summary>
	public class UpdateMapInstruction : Instruction
	{
		/// <summary>Private constructor.</summary>
		private UpdateMapInstruction() : base(PlayerType.neutral) { }

		/// <summary>Gets the available regions.</summary>
		public RegionUpdateInfo[] Regions { get; private set; }
			 
		/// <summary>Represents the instruction as String.</summary>
		public override string ToString()
		{
			return String.Format("update_map {0}", String.Join(" ", Regions.Select(r => r.ToString())));
		}

		public static UpdateMapInstruction Parse(string line)
		{
			return Detokenize(line.Split(' '));
		}
		public static UpdateMapInstruction Detokenize(params string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");

			if (tokens.Length % 3 != 1)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[0] != "update_map")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var instruction = new UpdateMapInstruction();

			var list = new List<RegionUpdateInfo>();
			for (int i = 1; i < tokens.Length; i += 3)
			{
				list.Add(RegionUpdateInfo.Create(tokens[i], tokens[i + 1], tokens[i + 2]));
			}
			instruction.Regions = list.ToArray();

			return instruction;
		}
	}
}
