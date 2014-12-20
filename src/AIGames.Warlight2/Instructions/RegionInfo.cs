using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Instructions
{
	/// <summary>Represents region info.</summary>
	public class RegionInfo
	{
		/// <summary>Constructor.</summary>
		public RegionInfo(int id, int superId)
		{
			this.Id = id;
			this.SuperRegionId = superId;
		}

		/// <summary>Gets the ID.</summary>
		public int Id { get; private set; }
		/// <summary>Gets the bonus armies reward.</summary>
		public int SuperRegionId { get; private set; }

		/// <summary>Represents the region info as string.</summary>
		public override string ToString()
		{
			return String.Format("{0} {1}", Id, SuperRegionId);
		}

		/// <summary>Creates a region info.</summary>
		public static RegionInfo Create(string id, string superId)
		{
			return new RegionInfo(Int32.Parse(id), Int32.Parse(superId));
		}
	}
}
