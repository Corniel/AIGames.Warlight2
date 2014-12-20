using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Cartography
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class Region
	{
		public Region(int id, SuperRegion parent)
		{
			this.Id = id;
			this.Neighbors = new Region[0];
			this.SuperRegion = Guard.NotNull(parent, "parent");
			this.SuperRegion.Add(this);
		}
		/// <summary>Gets the ID.</summary>
		public int Id { get; private set; }
		/// <summary>Returns true if the region is neigbhor of another super region, otherwise false.</summary>
		public bool IsBorder { get; private set; }
		/// <summary>Gets the parent super region.</summary>
		public SuperRegion SuperRegion { get; private set; }

		/// <summary>Gets the parent map.</summary>
		public Map Map { get { return this.SuperRegion.Map; } }

		/// <summary>Gets the neighbors.</summary>
		public Region[] Neighbors { get; private set; }

		/// <summary>Adds neighbor to this region.</summary>
		public void AddNeighbor(Region neighbor)
		{
			// Set neighbor to this.
			var temp = this.Neighbors.ToList();
			temp.Add(neighbor);
			this.Neighbors = temp.ToArray();
			if (neighbor.SuperRegion != this.SuperRegion)
			{
				this.IsBorder = true;
				neighbor.SuperRegion.AddNeighbor(this);
				this.SuperRegion.AddNeighbor(neighbor);
			}
			if (!neighbor.Neighbors.Contains(this))
			{
				neighbor.AddNeighbor(this);
			}
		}

		[ExcludeFromCodeCoverage, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format("Region[{0}], Super: {1}, Neighbors: {2}{3}",
					Id, 
					SuperRegion, 
					Neighbors.Length,
					IsBorder ? ", IsBorder": "");
			}
		}
	}
}
