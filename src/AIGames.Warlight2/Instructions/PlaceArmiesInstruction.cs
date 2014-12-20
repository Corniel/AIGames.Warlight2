using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
    /// <summary>Represents the place armies instruction.</summary>
    public class PlaceArmiesInstruction : Instruction
    {
        /// <summary>private constructor.</summary>
        public PlaceArmiesInstruction(PlayerType player, Int32 region, Int32 placement)
            : base(player)
        {
            this.Region = region;
            this.Placement = placement;
        }

        /// <summary>Gets the region.</summary>
        public Int32 Region { get; private set; }

        /// <summary>Gets the placement.</summary>
        public Int32 Placement { get; private set; }

        /// <summary>Represents the instruction as System.String.</summary>
        public override string ToString()
        {
            return String.Format("{0} place_armies {1} {2}", this.PlayerType, this.Region, this.Placement);
        }

        public static PlaceArmiesInstruction Detokenize(string[] tokens)
        {
			Guard.NotNullOrEmpty(tokens, "tokens");

            if (tokens.Length != 4)
            {
                throw new ArgumentException("tokens", "Token length is of invalid length.");
            }
            if (tokens[1] != "place_armies")
            {
                throw new ArgumentException("tokens", "Invalid token.");
            }
            return new PlaceArmiesInstruction(Player.Parse(tokens[0]), Int32.Parse(tokens[2]), Int32.Parse(tokens[3]));
        }
    }
}
