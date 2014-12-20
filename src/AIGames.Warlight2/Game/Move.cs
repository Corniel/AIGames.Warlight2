using AIGames.Warlight2.Cartography;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AIGames.Warlight2.Game
{
	/// <summary>Represents the state of a region.</summary>
	/// <remarks>
	///   Owner Source Target  Armies  Type
	/// | 0-7 | 8-15 | 16-23 | 24-54 | 55-56 | 
	/// </remarks>
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Move
	{
		/// <summary>Represnts 'no moves'.</summary>
		public static readonly Move NoMove = default(Move);

		private const int PositionSource = 8;
		private const int PositionTarget = 16;
		private const int PositionArmies = 24;
		private const int PositionType = 55;

		private Move(PlayerType owner, int source, int target, int armies, MoveType type)
		{
			m_Value = (int)owner & Bits.Mask08;
			m_Value += ((long)source & Bits.Mask08) << PositionSource;
			m_Value += ((long)target & Bits.Mask08) << PositionTarget;
			m_Value += ((long)armies & Bits.Mask31) << PositionArmies;
			m_Value += ((long)type) << PositionType;
		}

		/// <summary>The inner value.</summary>
		private long m_Value;

		/// <summary>The current owner of the region.</summary>
		public PlayerType Owner { get { return (PlayerType)(m_Value & Bits.Mask08); } }

		/// <summary>The ID of source region.</summary>
		public int SourceId { get { return (int)((m_Value >> PositionSource) & Bits.Mask08); } }

		/// <summary>The ID of target region.</summary>
		public int TargetId { get { return (int)((m_Value >> PositionTarget) & Bits.Mask08); } }

		/// <summary>The current amount of armies.</summary>
		public int Armies { get { return (int)(m_Value >> PositionArmies & Bits.Mask31); } }

		/// <summary>Gets the move type.</summary>
		public MoveType MoveType { get { return (MoveType)(m_Value >> PositionType); } }

		public override bool Equals(object obj) { return base.Equals(obj); }
		public override int GetHashCode() { return m_Value.GetHashCode(); }

		public static bool operator ==(Move l, Move r) { return l.Equals(r); }
		public static bool operator !=(Move l, Move r) { return !(l == r); }

		/// <summary>Represents the region state as debug string.</summary>
		[ExcludeFromCodeCoverage, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string DebuggerDisplay
		{
			get
			{
				if (m_Value == default(long)) { return "NoMove"; }

				var own = (int)Owner;
				switch (MoveType)
				{
					case MoveType.AttackTransfer:
						return String.Format("A/T({0}): {1}=>{2} ({3})", own, SourceId, TargetId, Armies);
					case MoveType.Select:
						return String.Format("Select({0}): {1}", own, SourceId);
					case MoveType.Stack:
						return String.Format("Stack({0}): {1} ({2})", own, SourceId, Armies);
					case MoveType.Set:
					default:
						return String.Format("Set({0}): {1} ({2})", own, SourceId, Armies);
				}
			}
		}

		/// <summary>Creates a select move.</summary>
		public static Move CreateSelect(PlayerType owner, Region region)
		{
			return CreateSelect(owner, region.Id);
		}
		/// <summary>Creates a select move.</summary>
		public static Move CreateSelect(PlayerType owner, int regionId)
		{
			return new Move(owner, regionId, 0, 0, MoveType.Select);
		}

		/// <summary>Creates a stack move.</summary>
		public static Move CreateStack(PlayerType owner, Region region, int armies)
		{
			return CreateStack(owner, region.Id, armies);
		}
		/// <summary>Creates a stack move.</summary>
		public static Move CreateStack(PlayerType owner, int regionId, int armies)
		{
			return new Move(owner, regionId, 0, armies, MoveType.Stack);
		}

		/// <summary>Creates a attack/transform move.</summary>
		public static Move CreateTransfer(PlayerType owner, Region source, Region target, int armies)
		{
			return CreateTransfer(owner, source.Id, target.Id, armies);
		}
		/// <summary>Creates a attack/transform move.</summary>
		public static Move CreateTransfer(PlayerType owner, int sourceId, int targetId, int armies)
		{
			return new Move(owner, sourceId, targetId, armies, MoveType.AttackTransfer);
		}

		/// <summary>Creates a stack move.</summary>
		public static Move CreateSet(PlayerType owner, Region region, int armies)
		{
			return CreateSet(owner, region.Id, armies);
		}
		/// <summary>Creates a stack move.</summary>
		public static Move CreateSet(PlayerType owner, int regionId, int armies)
		{
			return new Move(owner, regionId, 0, armies, MoveType.Set);
		}
	}
}
