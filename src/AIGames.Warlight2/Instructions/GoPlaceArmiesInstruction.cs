using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
    public class GoPlaceArmiesInstruction : Instruction
    {
         /// <summary>Private constructor.</summary>
        private GoPlaceArmiesInstruction() : base(PlayerType.neutral) { }

        /// <summary>Gets the allowed time out.</summary>
        public TimeSpan Timeout { get; private set; }

        /// <summary>Represents the instruction as String.</summary>
        public override string ToString()
        {
            return String.Format("go place_armies {0}", this.Timeout.TotalMilliseconds);
        }

        public static GoPlaceArmiesInstruction Parse(string line)
        {
            return Detokenize(line.Split(' '));
        }
        public static GoPlaceArmiesInstruction Detokenize(string[] tokens)
        {
			Guard.NotNullOrEmpty(tokens, "tokens");

            if (tokens.Length != 3)
            {
                throw new ArgumentException("tokens", "Token length is of invalid length.");
            }

            if (tokens[0] != "go" || tokens[1] != "place_armies")
            {
                throw new ArgumentException("tokens", "Invalid token.");
            }
           
            var instruction = new GoPlaceArmiesInstruction();

            instruction.Timeout = TimeSpan.FromMilliseconds(Int32.Parse(tokens[2]));

            return instruction;
        }
    }
}
