using AIGames.Warlight2.Game;
using AIGames.Warlight2.Instructions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AIGames.Warlight2.Cartography
{
	/// <summary>Represents a map.</summary>
	public class Map : IEnumerable<Region>
	{
		private Int32[,] m_Distances;
		private Dictionary<Int32, Region> m_Regions = new Dictionary<int, Region>();
		private Dictionary<Int32, SuperRegion> m_SuperRegions = new Dictionary<int, SuperRegion>();
		private List<Region> pickableStartRegions;

		/// <summary>Sets the picable start regions.</summary>
		public void SetPickableStartRegions(IEnumerable<int> ids)
		{
			pickableStartRegions = Select(ids).ToList();
		}

		/// <summary>Gets the picable start regions.</summary>
		public ICollection<Region> PickableStartRegions { get { return pickableStartRegions; } }

		/// <summary>Gets the distance between two regions.</summary>
		public Int32 GetDistance(Region regionA, Region regionB)
		{
			return m_Distances[regionA.Id, regionB.Id];
		}

		/// <summary>Gets a region based on its ID.</summary>
		public Region this[Int32 id] { get { return m_Regions[id]; } }

		public bool Contains(Region item) { return m_Regions.ContainsKey(item.Id); }
		/// <summary>Gets the total of regions.</summary>
		public int Count { get { return m_Regions.Count; } }

		public ICollection<SuperRegion> SuperRegions { get { return m_SuperRegions.Values; } }

		/// <summary>Selects the regions based on a collection ID's.</summary>
		public IEnumerable<Region> Select(params Int32[] ids)
		{
			foreach (var id in ids)
			{
				yield return m_Regions[id];
			}
		}
		/// <summary>Selects the regions based on a collection ID's.</summary>
		public IEnumerable<Region> Select(IEnumerable<Int32> ids)
		{
			foreach (var id in ids)
			{
				yield return m_Regions[id];
			}
		}

		/// <summary>Gets the super region bonus for a player excluding the default stack.</summary>
		public int GetSuperRegionBonus(PlayerType player, MapState state)
		{
			return this.SuperRegions
				.Where(super => super.All(region => state.HasOwner(region, player)))
				.Sum(super => super.BonusArmiesReward);
		}

		/// <summary>Adds a region.</summary>
		protected void Add(Region region)
		{
			m_Regions[region.Id] = Guard.NotNull(region, "region");
		}
		/// <summary>Adds a super region.</summary>
		protected void Add(SuperRegion superregion)
		{
			m_SuperRegions[superregion.Id] = Guard.NotNull(superregion, "superregion");
		}

		/// <summary>Finishes the map, by setting distances, and relations.</summary>
		public void Finish()
		{
			m_Distances = new Int32[this.Count + 1, this.Count + 1];

			foreach (var region in this)
			{
				foreach (var n in region.Neighbors)
				{
					m_Distances[region.Id, n.Id] = 1;
				}
			}

			for (int distance = 1; distance < this.Count; distance++)
			{
				foreach (var region in this)
				{
					foreach (var other in GetOnDistance(region, distance))
					{
						foreach (var neighbor in other.Neighbors.Where(n => n != region && m_Distances[region.Id, n.Id] == 0))
						{
							m_Distances[region.Id, neighbor.Id] = distance + 1;
						}
					}
				}
			}
		}

		private IEnumerable<Region> GetOnDistance(Region region, int distance)
		{
			return this.Where(r => GetDistance(region, r) == distance);
		}

		public IEnumerator<Region> GetEnumerator() { return m_Regions.Values.GetEnumerator(); }
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator() { return m_Regions.Values.GetEnumerator(); }

		/// <summary>Handles the setup instruction.</summary>
		public void Setup(SetupSuperRegionsInstruction instruction)
		{
			Guard.NotNull(instruction, "instruction");

			foreach (var info in instruction.SuperRegions)
			{
				Add(new SuperRegion(info.Id, info.BonusArmiesReward, this));
			}
		}
		/// <summary>Handles the setup instruction.</summary>
		public void Setup(SetupRegionsInstruction instruction)
		{
			Guard.NotNull(instruction, "instruction");

			foreach (var info in instruction.Regions)
			{
				var superRegion = m_SuperRegions[info.SuperRegionId];
				Add(new Region(info.Id, superRegion));
			}
		}

		/// <summary>Handles the setup instruction.</summary>
		public void Setup(SetupNeighborInstruction instruction)
		{
			Guard.NotNull(instruction, "instruction");

			foreach (var info in instruction.Neighbors)
			{
				var region = this[info.Id];

				foreach (var nId in info.Neighbors)
				{
					var neighbor = this[nId];
					region.AddNeighbor(neighbor);
				}
			}
		}

		/// <summary>Creates an inital state.</summary>
		public MapState ToInitialRegionState()
		{
			var state = new MapState(this.Count);

			foreach (var region in this)
			{
				state.Set(region, PlayerType.neutral, Settings.InitialArmies);
			}
			return state;
		}

		
	}
}
