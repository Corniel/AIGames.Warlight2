using AIGames.Warlight2.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Cartography
{
	public static class RegionCollectionExtensions
	{

		/// <summary>Gets regions that have a border with another super region.</summary>
		public static IEnumerable<Region> GetBorders(this IEnumerable<Region> regions)
		{
			return regions.Where(r => r.IsBorder);
		}

		/// <summary>Gets neighbors regions.</summary>
		public static IEnumerable<Region> GetIncludingNeighbors(this IEnumerable<Region> regions)
		{
			return regions.SelectMany(r => r.Neighbors).Distinct();
		}

		/// <summary>Gets neighbors regions.</summary>
		public static IEnumerable<Region> GetNeighbors(this IEnumerable<Region> regions)
		{
			return regions.GetIncludingNeighbors().Where(r => !regions.Contains(r));
		}

		/// <summary>Gets the regions for the specified owner.</summary>
		public static IEnumerable<Region> GetRegionsForPlayer(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.Where(region => state.HasOwner(region, owner));
		}

		/// <summary>Gets the regions that are enermies for the owner excluding neutral.</summary>
		public static IEnumerable<Region> GetRegionsForPlayerWithEnermies(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.Where(region =>
				state.HasOwner(region, owner) &&
				region.Neighbors
				.Any(neighbor =>
					state.Owner(neighbor) != PlayerType.neutral &&
					!state.HasOwner(neighbor, owner)));
		}

		/// <summary>Gets the regions that are enermies for the owner excluding neutral.</summary>
		public static IEnumerable<Region> GetRegionsForPlayerWithOpponents(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.Where(region =>
				state.HasOwner(region, owner) &&
				region.Neighbors
				.Any(neighbor => !state.HasOwner(neighbor, owner)));
		}

		/// <summary>Gets the regions that are opponents for the owner inlcuding neutral.</summary>
		public static IEnumerable<Region> GetOpponentsForPlayer(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.Where(region => !state.HasOwner(region, owner));
		}

		/// <summary>Gets the regions that are enermies for the owner excluding neutral.</summary>
		public static IEnumerable<Region> GetEnemriesForPlayer(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.Where(region => state.Owner(region) != PlayerType.neutral && !state.HasOwner(region, owner));
		}

		/// <summary>Gets the attacking force of the regions.</summary>
		public static int AttackingForce(this IEnumerable<Region> regions, MapState state)
		{
			return regions
				.Where(region => state.Owner(region) != PlayerType.neutral)
				.Sum(region => state.Armies(region) - 1);
		}

		/// <summary>Gets the region count that are opponents for the owner inlcuding neutral.</summary>
		public static int GetOpponentCountForPlayer(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.Count(region => !state.HasOwner(region, owner));
		}

		/// <summary>Returns true if all regions have the specified owener.</summary>
		public static bool HaveOwner(this IEnumerable<Region> regions, PlayerType owner, MapState state)
		{
			return regions.All(region => state.HasOwner(region, owner));
		}
	}
}
