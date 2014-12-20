using AIGames.Warlight2.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AIGames.Warlight2.Cartography
{
	/// <summary>Represents a state of the map.</summary>
	public struct MapState : IEnumerable<RegionState>, IEquatable<MapState>
	{
		/// <summary>Constructs a new map state.</summary>
		public MapState(int size)
		{
			m_Regions = new RegionState[size];
			m_Data = 0;
		}

		private RegionState[] m_Regions;

		/// <summary>the inner data.</summary>
		/// <remarks>
		///           (      depth      )
		///   hash   player sub   round
		/// | 0 - 21 | 22 | 23 | 24 - 31 | 
		/// </remarks>
		private uint m_Data;

		private const uint MaskHash = Bits.Mask22;

		private const uint MaskDepth = Bits.Mask10;
		private const uint MaskPlayerToMove = Bits.Mask01;
		private const uint MaskSubRound = Bits.Mask01;
		private const uint MaskRound = Bits.Mask08;

		private const int PositionHash = 0;
		private const int PositionDepth = 22;
		private const int PositionPlayerToMove = 22;
		private const int PositionSubRound = 23;
		private const int PositionRound = 24;

		private const int MaskHashSwitch = 7;

		#region Collection and IEnumerable

		/// <summary>Gets the region state based on region.</summary>
		/// <remarks>
		/// The identifier is one-based.
		/// </remarks>
		public RegionState this[Region region]
		{
			get
			{
				Guard.NotNull(region, "region");
				return this[region.Id];
			}
		}
		/// <summary>Gets the region state based on its identifier.</summary>
		/// <remarks>
		/// The identifier is one-based.
		/// </remarks>
		public RegionState this[int regionId]
		{
			get
			{
				GuardRegionId(regionId);
				return m_Regions[regionId - 1];
			}
			private set
			{
				GuardRegionId(regionId);
				m_Regions[regionId - 1] = value;
			}
		}
		private void GuardRegionId(int regionId)
		{
#if DEBUG
			if (regionId < 1 || m_Regions == null || m_Regions.Length < regionId) { throw new ArgumentOutOfRangeException(); }
#endif
		}

		/// <summary>Gets the number of regions.</summary>
		public int Count { get { return m_Regions == null ? 0 : m_Regions.Length; } }

		/// <summary>Gets the enumerator for the region states.</summary>
		public IEnumerator<RegionState> GetEnumerator() { return m_Regions.AsEnumerable().GetEnumerator(); }
		/// <summary>Gets the enumerator for the region states.</summary>
		IEnumerator IEnumerable.GetEnumerator() { return m_Regions.GetEnumerator(); }

		#endregion

		/// <summary>Gets the round.</summary>
		public int Round { get { return (int)((m_Data >> PositionRound) & MaskRound); } }
		/// <summary>Gets the sub round.</summary>
		public SubRoundType SubRound { get { return (SubRoundType)((m_Data >> PositionSubRound) & MaskSubRound); } }
		/// <summary>Gets the player to move.</summary>
		public PlayerType PlayerToMove { get { return PlayerType.neutral; } }
		/// <summary>Gets the depth of the state.</summary>
		/// <remarks>
		/// Combines round, subround and player to move.
		/// </remarks>
		public uint Depth { get { return m_Data >> PositionDepth; } }

		/// <summary>Get the armies of the region.</summary>
		public int Armies(Region region)
		{
			Guard.NotNull(region, "region");
			return Armies(region.Id);
		}
		/// <summary>Get the armies of the region.</summary>
		public int Armies(int regionId) { return this[regionId].Armies; }

		/// <summary>Get the owner of the region.</summary>
		public PlayerType Owner(Region region)
		{
			Guard.NotNull(region, "region");
			return Owner(region.Id);
		}
		/// <summary>Get the owner of the region.</summary>
		public PlayerType Owner(int regionId) { return this[regionId].Owner; }
		/// <summary>Returns true if the region has the specified owner.</summary>
		public bool HasOwner(Region region, PlayerType owner)
		{
			return Owner(region) == owner;
		}

		/// <summary>Creates a copy of the map state.</summary>
		[DebuggerStepThrough]
		public MapState Copy()
		{
			var data = m_Data;
			var regions = new RegionState[m_Regions.Length];
			Array.Copy(m_Regions, regions, m_Regions.Length);

			return new MapState() { m_Regions = regions, m_Data = data };
		}

		/// <summary>Applies a move.</summary>
		public void Apply(Move move, Map map)
		{
			switch (move.MoveType)
			{
				case MoveType.Set:
					Set(move.SourceId, (int)move.Owner, move.Armies);
					break;

				case MoveType.Select:
					Set(move.SourceId, (int)move.Owner, Settings.InitialArmies);
					break;

				case MoveType.Stack:
					Stack(move.SourceId, (ushort)move.Owner, move.Armies);
					break;

				case MoveType.AttackTransfer:
				default:
					AttackOrTransfer(move.SourceId, move.TargetId, (ushort)move.Owner, move.Armies, map);
					break;
			}
		}
		/// <summary>Resets the number of armies of the region.</summary>
		public void Set(Region region, PlayerType owner, int armies)
		{
			Set(region.Id, (int)owner, armies);
		}
		/// <summary>Resets the number of armies of the region.</summary>
		public void Set(int regionId, int owner, int armies)
		{
			unchecked
			{
				var oldRegion = this[regionId];
				var newRegion = RegionState.Create(armies, owner);
				this[regionId] = newRegion;

				// update the hash.
				XOrHash(oldRegion, newRegion, regionId);
			}
		}
		/// <summary>Stacks a region.</summary>
		private void Stack(int regionId, ushort owner, int armies)
		{
			unchecked
			{
				var oldRegion = this[regionId];
				var oldArmies = Armies(regionId);
				var newArmies = (ushort)(oldArmies + armies);

				// set and create.
				var newRegion = RegionState.Create(newArmies, owner);
				this[regionId] = newRegion;

				// update the hash.
				XOrHash(oldRegion, newRegion, regionId);
			}
		}
		/// <summary>Attacks or transfer to a region.</summary>
		private void AttackOrTransfer(int sourceId, int targetId, ushort owner, int armies, Map map)
		{
			unchecked
			{
				var sOldRegion = this[sourceId];
				var tOldRegion = this[targetId];

				var sOldArmies = (ushort)Armies(sourceId);
				var tOldArmies = (ushort)Armies(targetId);

				ushort sNewArmies;
				ushort tNewArmies;

				var tOldOwner = (ushort)Owner(targetId);
				var tNewOwner = tOldOwner;

				// attack
				if (owner != tOldOwner)
				{
					int attackersLeave;
					int targetNew;
					var success = Combat.ApplyAverageAttack(armies, tOldArmies, out attackersLeave, out targetNew);
					sNewArmies = (ushort)Math.Max(Settings.MinimumArmies, sOldArmies - attackersLeave);
					tNewArmies = (ushort)targetNew;
					if (success)
					{
						tNewOwner = owner;
					}
				}
				else
				{
					sNewArmies = (ushort)Math.Max(Settings.MinimumArmies, sOldArmies - armies);
					tNewArmies = (ushort)(tOldArmies + armies);
				}

				// set and create.
				var sNewRegion = RegionState.Create(sNewArmies, owner);
				var tNewRegion = RegionState.Create(tNewArmies, tNewOwner);

				this[sourceId] = sNewRegion;
				this[targetId] = tNewRegion;

				// update the hash.
				XOrHash(sOldRegion, sNewRegion, sourceId);
				XOrHash(tOldRegion, tNewRegion, targetId);
			}
		}
		/// <summary>Applies an XOR on the hash with the switched value.</summary>
		private void XOrHash(RegionState oldRegion, RegionState newRegion, int swch)
		{
			unchecked
			{
				// Fit the switch in the length.
				swch &= MaskHashSwitch;

				// create the change.
				var change = oldRegion.GetHashCode() ^ newRegion.GetHashCode();

				// roulate the change.
				change = change << swch | change >> (PositionDepth - swch);

				// apply the change.
				m_Data ^= (uint)(MaskHash & change);
			}
		}

		/// <summary>Sets the next step.</summary>
		public void NextStep(int player)
		{
			unchecked
			{
				var depth = Depth;
				// add 2 extra for round 0 to 1.
				depth ^= (uint)(depth + (depth == 1 ? 3 : 1));
				m_Data ^= (depth << PositionDepth);
			}
		}
		/// <summary>Sets round state.</summary>
		/// <param name="round">
		/// The round.
		/// </param>
		/// <param name="subround">
		/// The sub round.
		/// </param>
		/// <param name="playerToMove">
		/// The player to move.
		/// </param>
		public void SetRoundSubRoundAndPlayerToMove(int round, SubRoundType subround, PlayerType playerToMove)
		{
			m_Data &= MaskHash;

			m_Data |= (uint)(round << PositionRound);
			m_Data |= (uint)((int)subround << PositionSubRound);
			if (playerToMove == (((round & 1) == 1) ? PlayerType.player2 : PlayerType.player1))
			{
				m_Data |= Bits.Mask01 << PositionPlayerToMove;
			}
		}

		/// <summary>Gets the hash code.</summary>
		public override int GetHashCode() { return m_Data.GetHashCode(); }

		/// <summary>Returns true if object equals this map state, otherwise false.</summary>
		public override bool Equals(object obj)
		{
#if DEBUG
			return obj != null && obj is MapState && Equals((MapState)obj);
#else
						  return Equals((MapState)obj);
#endif
		}
		/// <summary>Returns true if other map state equals this one, otherwise false.</summary>
		public bool Equals(MapState other)
		{
			if (!m_Data.Equals(other.m_Data) || Count != other.Count) { return false; }

			for (var i = 0; i < Count; i++)
			{
				if (!m_Regions[i].Equals(other.m_Regions[i])) { return false; }
			}
			return true;
		}

		public static MapState Create(IEnumerable<RegionState> regions)
		{
			var map = new MapState() 
			{ 
				m_Regions = regions.ToArray(),
				m_Data = 0,
			};

			for (var i = 0; i < map.m_Regions.Length; i++)
			{
				map.XOrHash(RegionState.Unknown, map[i], i + 1);
			}
			return map;
		}

		/// <summary>represents the map state as debug string.</summary>
		[ExcludeFromCodeCoverage, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string DebuggerDisplay
		{
			get
			{
				return String.Format("MapState[{0}.{1}] {2}, Regions: {3}", Round, SubRound, PlayerToMove, Count);
			}
		}
	}
}
