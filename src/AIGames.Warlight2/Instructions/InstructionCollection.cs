using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Instructions
{
    public class InstructionCollection : Instruction
    {
        /// <summary>Constructs a collection of instructions.</summary>
        public InstructionCollection(IEnumerable<Instruction> instructions) : base(PlayerType.neutral)
        {
            this.Instructions = instructions.ToArray();
        }

        /// <summary>Gets the instructions</summary>
        public Instruction[] Instructions { get; protected set; }

        /// <summary>Represents the instruction as System.String.</summary>
        public override string ToString()
        {
            return String.Join(", ", this.Instructions.Select(instruction => instruction.ToString()));
        }

        /// <summary>Represents the instruction as debug string.</summary>
        public override String DebugToString()
        {
            var name =  Instructions.First().GetType().Name;
            name = name.Substring(0, name.IndexOf("Instruction"));
            return String.Format("{0}[{2}]: {1}", name, ToString(), Instructions.Length);
        }
    }
}
