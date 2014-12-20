using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
	public class SettingsStartingArmiesInstruction : Instruction
	{
		/// <summary>Private constructor.</summary>
		private SettingsStartingArmiesInstruction() : base(PlayerType.neutral) { }

		/// <summary>Gets the starting armies for the current round.</summary>
		public Int32 Armies { get; private set; }

		/// <summary>Represents the instruction as String.</summary>
		public override string ToString()
		{
			return String.Format("settings starting_armies {0}", Armies);
		}

		public static SettingsStartingArmiesInstruction Parse(string line)
		{
			return Detokenize(line.Split(' '));
		}
		public static SettingsStartingArmiesInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");

			if (tokens.Length != 3)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[0] != "settings" || tokens[1] != "starting_armies")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var instruction = new SettingsStartingArmiesInstruction();
			instruction.Armies = Int32.Parse(tokens[2]);
			return instruction;
		}
	}
}
