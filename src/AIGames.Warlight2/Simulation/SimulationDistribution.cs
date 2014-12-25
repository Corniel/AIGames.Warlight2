using AIGames.Warlight2.Cartography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.Simulation
{
	public class SimulationDistribution
	{
		public RegionState Source { get ;set;}

		public Dictionary<RegionState, int> Success { get; set; }
		public Dictionary<SimulationOutcome, int> Failure { get; set; }

		public int SuccessCount { get { return Success.Values.Sum(); } }
		public int FailureCount { get { return Failure.Values.Sum(); } }
	}
}
