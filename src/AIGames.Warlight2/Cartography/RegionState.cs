using AIGames.Warlight2.Game;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace AIGames.Warlight2.Cartography
{
	[DebuggerDisplay("{DebuggerDisplay}"), Serializable]
	public struct RegionState : ISerializable, IEquatable<RegionState>
	{
		private const ushort MaxArmies = ushort.MaxValue >> 2;

		public static readonly RegionState Unknown = default(RegionState);

		/// <summary>The underlying value.</summary>
		private ushort m_Value;

		/// <summary>Gets the amount of armies.</summary>
		public int Armies { get { return m_Value >> 2; } }

		/// <summary>Get the owner of the region.</summary>
		public PlayerType Owner { get { return (PlayerType)(m_Value & 3); } }

		#region Serializable

		/// <summary>Serialization constructor.</summary>
		private RegionState(SerializationInfo info, StreamingContext context)
		{
			m_Value = info.GetUInt16("Value");
		}
		/// <summary>Gets the object data to serialize.</summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Value", m_Value);
		}
		#endregion

		/// <summary>Gets the hash code.</summary>
		public override int GetHashCode() { return m_Value; }

		/// <summary>Returns true if object equals this region state, otherwise false.</summary>
		public override bool Equals(object obj)
		{
#if DEBUG
			return obj != null && obj is RegionState && Equals((RegionState)obj);
#else
						  return Equals((RegionState)obj);
#endif
		}

		/// <summary>Returns true if other region state equals this one, otherwise false.</summary>
		public bool Equals(RegionState other) { return m_Value.Equals(other.m_Value); }

		/// <summary>Returns true if left and right are equal, otherwise false.</summary>
		public static bool operator ==(RegionState l, RegionState r) { return l.Equals(r); }
		/// <summary>Returns true if left and right are not equal, otherwise false.</summary>
		public static bool operator !=(RegionState l, RegionState r) { return !(l == r); }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture,
						 "{0} {1}",
						 Armies == 0 ? "?" : Armies.ToString(), Owner);
			}
		}

		/// <summary>Creates a ne region state.</summary>
		/// <param name="owner">
		/// The owner of the state.
		/// </param>
		/// <param name="armies">
		/// The amount of armies.
		/// </param>
		public static RegionState Create(int armies, PlayerType owner)
		{
			return Create(armies, (int)owner);
		}

		/// <param name="owner">
		/// The owner of the state.
		/// </param>
		/// <param name="armies">
		/// The amount of armies.
		/// </param>
		internal static RegionState Create(int armies, int owner)
		{
#if DEBUG
			if (armies < 0 || armies > MaxArmies) { throw new ArgumentOutOfRangeException("Amount of armies should be in the range [0, 16383]."); }
			if (owner < 0 || owner > 2) { throw new ArgumentOutOfRangeException("Owner should  be in the range [0, 2]."); }
#endif
			return new RegionState() { m_Value = (ushort)(owner | armies << 2) };
		}
	}
}
