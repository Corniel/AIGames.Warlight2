using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AIGames.Warlight2.Cartography
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class SuperRegion : IEnumerable<Region>
	{
		private Dictionary<Int32, Region> m_Regions = new Dictionary<int, Region>();

		public SuperRegion(int id, int bonus, Map map)
		{
			Id = id;
			BonusArmiesReward = bonus;
			Map = Guard.NotNull(map, "map");
			Neighbors = new Region[0];
			SelfAndNeighbors = new Region[0];
		}

		/// <summary>Gets the ID.</summary>
		public Int32 Id { get; private set; }
		/// <summary>Gets the armies reward.</summary>
		public int BonusArmiesReward { get; private set; }
		/// <summary>Gets the parent map.</summary>
		public Map Map { get; private set; }

		/// <summary>Gets the neighbors.</summary>
		public Region[] Neighbors { get; private set; }
		/// <summary>Gets the neighbors.</summary>
		public Region[] SelfAndNeighbors { get; private set; }

		/// <summary>Adds region to super region.</summary>
		public void Add(Region region)
		{
			m_Regions[region.Id] = Guard.NotNull(region, "region");
		}
		/// <summary>Adds neighbor to this region.</summary>
		public void AddNeighbor(Region neighbor)
		{
			if (!this.Neighbors.Contains(neighbor))
			{
				// Set neighbor to this.
				var temp = this.Neighbors.ToList();
				temp.Add(neighbor);
				this.Neighbors = temp.ToArray();
			}
			if (!this.SelfAndNeighbors.Contains(neighbor))
			{
				// Set neighbor to this.
				var temp = this.SelfAndNeighbors.ToList();
				temp.Add(neighbor);
				this.SelfAndNeighbors = temp.ToArray();
			}
		}

		/// <summary>Gets a region based on its ID.</summary>
		public Region this[Int32 id] { get { return m_Regions[id]; } }

		/// <summary>Gets the number of regions in this super region.</summary>
		public int Count { get { return m_Regions.Count; } }
		
		public IEnumerator<Region> GetEnumerator() { return m_Regions.Values.GetEnumerator(); }
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator() { return m_Regions.Values.GetEnumerator(); }

		[ExcludeFromCodeCoverage, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format("SuperRegion[{0}], Bonus: {1}, Regions: {2}", Id, BonusArmiesReward, Count);
			}
		}
	}
}
