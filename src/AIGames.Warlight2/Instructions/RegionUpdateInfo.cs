using AIGames.Warlight2.Game;
using System;

namespace AIGames.Warlight2.Instructions
{
    /// <summary>Represents region update info.</summary>
    public class RegionUpdateInfo
    {
        /// <summary>Constructor.</summary>
        public RegionUpdateInfo(Int32 id, PlayerType owner, Int32 armies)
        {
            this.Id = id;
            this.Owner = owner;
            this.Armies = armies;
        }

        /// <summary>Gets the ID.</summary>
        public Int32 Id { get; private set; }
        /// <summary>Gets the Owner.</summary>
        public PlayerType Owner { get; private set; }
        /// <summary>Gets the armies.</summary>
        public Int32 Armies { get; private set; }

        /// <summary>Represents the region update info as string.</summary>
        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Id, Owner, Armies);
        }

        /// <summary>Creates a region update info.</summary>
        public static RegionUpdateInfo Create(string id, string owner, string armies)
        {
            return new RegionUpdateInfo(Int32.Parse(id), Player.Parse(owner), Int32.Parse(armies));
        }
    }
}
