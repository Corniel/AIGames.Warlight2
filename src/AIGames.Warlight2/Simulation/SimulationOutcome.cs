using AIGames.Warlight2.Cartography;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Simulation
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct SimulationOutcome
	{
		public SimulationOutcome(RegionState source, RegionState target)
		{
			m_Source = source;
			m_Target = target;
		}
		private RegionState m_Source;
		private RegionState m_Target;

		public RegionState Source { get { return m_Source; } }
		public RegionState Target { get { return m_Target; } }

		/// <summary>Returns true if the source ande target have the same owner.</summary>
		public bool HaveSameOwner { get { return m_Source.Owner == m_Target.Owner; } }

		private string DebuggerDisplay
		{
			get
			{
				return String.Format("src: {0}, tar: {1}", m_Source.DebuggerDisplay, m_Target.DebuggerDisplay);
			}
		}
	}
}
