using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
	/// <summary>Represents the base of an attack or trasfer instruction.</summary>
	public class AttackTransferInstruction : Instruction
	{
		/// <summary>Protected constructor.</summary>
		public AttackTransferInstruction(PlayerType owner, int source, int target, int armies)
			: base(owner)
		{
			this.Source = source;
			this.Target = target;
			this.Armies = armies;
		}

		/// <summary>Gets the source region.</summary>
		public Int32 Source { get; private set; }

		/// <summary>Gets the target region.</summary>
		public Int32 Target { get; private set; }

		/// <summary>Gets the armies involved.</summary>
		public Int32 Armies { get; private set; }

		/// <summary>Represents the instruction as System.String.</summary>
		public override string ToString()
		{
			return String.Format("{0} attack/transfer {1} {2} {3}",
				this.PlayerType, this.Source, this.Target, this.Armies);
		}

		public static AttackTransferInstruction Detokenize(string[] tokens)
		{
			Guard.NotNullOrEmpty(tokens, "tokens");

			if (tokens.Length != 5)
			{
				throw new ArgumentException("tokens", "Token length is of invalid length.");
			}
			if (tokens[1] != "attack/transfer")
			{
				throw new ArgumentException("tokens", "Invalid token.");
			}
			return new AttackTransferInstruction(
				Player.Parse(tokens[0]),
				Int32.Parse(tokens[2]),
				Int32.Parse(tokens[3]),
				Int32.Parse(tokens[4]));
		}
	}
}
