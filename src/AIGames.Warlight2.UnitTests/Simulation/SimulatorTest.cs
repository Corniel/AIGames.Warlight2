using AIGames.Warlight2.Cartography;
using AIGames.Warlight2.Game;
using AIGames.Warlight2.Simulation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troschuetz.Random.Generators;

namespace AIGames.Warlight2.UnitTests.Simulation
{
	[TestFixture]
	public class SimulatorTest
	{
		[Test]
		public void Simulate_20Vs11_10000Runs()
		{
			var source = RegionState.Create(20, PlayerType.player1);
			var target = RegionState.Create(11, PlayerType.player2);

			var rnd = new MT19937Generator(17);
			var simulator = new Simulator(rnd);

			var sw = new Stopwatch();
			sw.Start();

			var act = simulator.Simulate(source, target, 19, 10000);

			sw.Stop();

			Console.WriteLine(sw.Elapsed.TotalMilliseconds);

			Console.WriteLine("success: {0}, failure: {1}", act.SuccessCount, act.FailureCount);
		}
	}
}
