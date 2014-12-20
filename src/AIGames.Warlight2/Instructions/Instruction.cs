using AIGames.Warlight2.Game;
using System;
using System.Diagnostics;

namespace AIGames.Warlight2.Instructions
{
    [DebuggerDisplay("{DebugToString()}")]
    public abstract class Instruction
    {
        protected Instruction(PlayerType player)
        {
            this.PlayerType = player;
        }

        /// <summary>Gets the player of the instruction.</summary>
        public PlayerType PlayerType { get; private set; }

        /// <summary>Represents the instruction as debug string.</summary>
        public virtual String DebugToString()
        {
            var name = GetType().Name;
            name = name.Substring(0, name.IndexOf("Instruction"));
            return String.Format("{0}: {1}", name, ToString());
        }
    }
}
