using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIGames.Warlight2.Instructions
{
	public class OpponentMovesInstruction : InstructionCollection
	{
		/// <summary>Constructs a collection of instructions.</summary>
		protected OpponentMovesInstruction(IEnumerable<Instruction> instructions)
			: base(instructions) { }

		/// <summary>Represents the instruction as System.String.</summary>
		public override string ToString()
		{
			return String.Format("opponent_moves {0}",
				String.Join(" ", this.Instructions.Select(instruction => instruction.ToString())));
		}

		public static OpponentMovesInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");

			if (tokens[0] != "opponent_moves")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}

			var instructions = new List<Instruction>();

			var index = 1;
			while (index < tokens.Length)
			{
				switch (tokens[index + 1])
				{
					case "place_armies":
						instructions.Add(PlaceArmiesInstruction.Detokenize(tokens.Skip(index).Take(4).ToArray()));
						index += 4;
						break;
					case "attack/transfer":
						instructions.Add(AttackTransferInstruction.Detokenize(tokens.Skip(index).Take(5).ToArray()));
						index += 5;
						break;
					default:
						throw new ArgumentException("tokens", "Invalid token.");
				}
			}
			return new OpponentMovesInstruction(instructions);
		}
	}
}
