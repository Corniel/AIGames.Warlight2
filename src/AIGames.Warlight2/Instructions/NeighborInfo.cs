using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Instructions
{
	/// <summary>Represents neighbor info.</summary>
	public class NeighborInfo
	{
		/// <summary>Constructor.</summary>
		public NeighborInfo(int id, IEnumerable<int> neighbors)
		{
			this.Id = id;
			this.Neighbors = neighbors.ToArray();
		}

		/// <summary>Gets the ID.</summary>
		public int Id { get; private set; }
		/// <summary>Gets the neighbors.</summary>
		public int[] Neighbors { get; private set; }

		/// <summary>Represents the neighbor info as string.</summary>
		public override string ToString()
		{
			return String.Format("{0} {1}", Id, String.Join(",", Neighbors.AsEnumerable()));
		}

		/// <summary>Creates a neighbor info.</summary>
		public static NeighborInfo Create(string id, string neighbors)
		{
			return new NeighborInfo(Int32.Parse(id), neighbors.Split('.').Select(s=> Int32.Parse(s)));
		}
	}
}
