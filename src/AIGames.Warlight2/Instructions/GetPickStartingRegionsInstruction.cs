using AIGames.Warlight2.Game;
using System;
using System.Linq;

namespace AIGames.Warlight2.Instructions
{
	/// <summary>Represents a get pick starting regions instruction.</summary>
	public class GetPickStartingRegionsInstruction : Instruction
	{
		/// <summary>Private constructor.</summary>
		private GetPickStartingRegionsInstruction() : base(PlayerType.neutral) { }

		/// <summary>Gets the allowed time out.</summary>
		public TimeSpan Timeout { get; private set; }

		/// <summary>Gets the pickable starting regions.</summary>
		public Int32[] Regions { get; private set; }

		/// <summary>Represents the instruction as String.</summary>
		public override string ToString()
		{
			return String.Format("pick_starting_regions {0} {1}", 
				this.Timeout.TotalMilliseconds,
				String.Join(" ", Regions.Select(r => r.ToString())));
		}

		public static GetPickStartingRegionsInstruction Parse(string line)
		{
			return Detokenize(line.Split(' '));
		}
		public static GetPickStartingRegionsInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");

			if (tokens.Length != 14)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[0] != "pick_starting_regions")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var instruction = new GetPickStartingRegionsInstruction();

			instruction.Timeout = TimeSpan.FromMilliseconds(Int32.Parse(tokens[1]));

			instruction.Regions = tokens.Skip(2).Select(token => Int32.Parse(token)).ToArray();

			return instruction;
		}
	}
}
