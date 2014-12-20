using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AIGames.Warlight2.Instructions
{
    public class InstructionWriter
    {
        public InstructionWriter() : this(Console.Out){ }

        public InstructionWriter(TextWriter textwriter)
        {
            this.Writer = textwriter;
            this.Instructions = new List<Instruction>();
        }

        public List<Instruction> Instructions { get; private set; }

        public TextWriter Writer { get; private set; }

        public void Add(Instruction instruction)
        {
            this.Writer.WriteLine(instruction);
            this.Instructions.Add(instruction);
        }

        public void AddRange(IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                Add(instruction);
            }
        }
    }
}
