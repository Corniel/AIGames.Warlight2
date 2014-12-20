using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
    public class GoAttackTransferInstruction : Instruction
    {
         /// <summary>Private constructor.</summary>
        private GoAttackTransferInstruction() : base(PlayerType.neutral) { }

        /// <summary>Gets the allowed time out.</summary>
        public TimeSpan Timeout { get; private set; }

        /// <summary>Represents the instruction as String.</summary>
        public override string ToString()
        {
            return String.Format("go attack/transfer {0}", this.Timeout.TotalMilliseconds);
        }

        public static GoAttackTransferInstruction Parse(string line)
        {
            return Detokenize(line.Split(' '));
        }
        public static GoAttackTransferInstruction Detokenize(string[] tokens)
        {
			Guard.NotNullOrEmpty(tokens, "tokens");

            if (tokens.Length != 3)
            {
                throw new ArgumentException("tokens", "Token length is of invalid length.");
            }
            if (tokens[0] != "go" || tokens[1] != "attack/transfer")
            {
                throw new ArgumentException("tokens", "Invalid token.");
            }

            var instruction = new GoAttackTransferInstruction();

            instruction.Timeout = TimeSpan.FromMilliseconds(Int32.Parse(tokens[2]));

            return instruction;
        }
    }
}
