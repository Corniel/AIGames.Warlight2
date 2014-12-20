using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Instructions
{
	/// <summary>Represents super region info.</summary>
	public class SuperRegionInfo
	{
		/// <summary>Constructor.</summary>
		public SuperRegionInfo(int id, int reward)
		{
			this.Id = id;
			this.BonusArmiesReward = reward;
		}

		/// <summary>Gets the ID.</summary>
		public int Id { get; private set; }
		/// <summary>Gets the bonus armies reward.</summary>
		public int BonusArmiesReward { get; private set; }

		/// <summary>Represents the super region info as string.</summary>
		public override string ToString()
		{
			return String.Format("{0} {1}", Id, BonusArmiesReward);
		}

		/// <summary>Creates a super region info.</summary>
		public static SuperRegionInfo Create(string id, string reward)
		{
			return new SuperRegionInfo(Int32.Parse(id), Int32.Parse(reward));
		}
	}
}
