using AIGames.Warlight2.Game;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Cartography
{
	public static class SuperRegionCollectionExtensions
	{
		public static IEnumerable<SuperRegion> GetConquerables(this IEnumerable<SuperRegion> superregions, PlayerType player, MapState state)
		{
			return superregions
					.Where(s => s.SelfAndNeighbors
						.Any(region => state.HasOwner(region, player)) && s.SelfAndNeighbors
						.Any(region => !state.HasOwner(region, player)));
		}
	}
}
