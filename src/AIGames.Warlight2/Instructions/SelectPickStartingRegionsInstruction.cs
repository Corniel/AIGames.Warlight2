using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AIGames.Warlight2.Instructions
{
    /// <summary>Represents the preferred starting regions instruction.</summary>
    public class SelectPickStartingRegionsInstruction : Instruction
    {
        /// <summary>Represents a valid instruction.</summary>
        public static readonly Regex Pattern = new Regex("^[1-9][0-9]*( [1-9][0-9]*){5}$", RegexOptions.Compiled);

        /// <summary>private constructor.</summary>
        public SelectPickStartingRegionsInstruction(PlayerType player, IEnumerable<int> regions) : base(player)
        {
            this.PreferredStartingRegions = regions.ToArray();
        }

        /// <summary>Gets the Preferred Starting Regions</summary>
        public int[] PreferredStartingRegions { get; private set; }

        /// <summary>Represents the instruction as System.String.</summary>
        public override string ToString()
        {
            return String.Join(" ", this.PreferredStartingRegions);
        }
    }
}
