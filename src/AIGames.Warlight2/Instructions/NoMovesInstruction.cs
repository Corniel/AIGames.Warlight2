using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
    /// <summary>Represents a no move instruction.</summary>
    public class NoMovesInstruction : Instruction
    {
        /// <summary>Initializes a new instruction.</summary>
        public NoMovesInstruction() : base(PlayerType.neutral) { }

        /// <summary>Represents the instruction as System.String.</summary>
        public override string ToString()
        {
            return "No moves";
        }
    }
}
