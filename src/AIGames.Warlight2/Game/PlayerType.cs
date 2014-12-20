using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Game
{
	/// <summary>The player type.</summary>
	public enum PlayerType
	{
		/// <summary>No player.</summary>
		neutral = 0,
		/// <summary>Player 1.</summary>
		player1 = 1,
		/// <summary>Player 2.</summary>
		player2 = 2,
	}

	/// <summary>Extensions on player type.</summary>
	public static class Player
	{
		/// <summary>Get the other player.</summary>
		public static PlayerType Other(this PlayerType current)
		{
			switch (current)
			{
				case PlayerType.player1: return PlayerType.player2;
				case PlayerType.player2: return PlayerType.player1;
			}
			return PlayerType.neutral;
		}

		public static PlayerType Parse(string player)
		{
			return (PlayerType)Enum.Parse(typeof(PlayerType), player, true);
		}
		public static PlayerType FromId(int id)
		{
			return (PlayerType)id;
		}
	}
}
