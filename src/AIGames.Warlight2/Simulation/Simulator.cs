using AIGames.Warlight2.Cartography;
using System;
using System.Collections.Generic;
using Troschuetz.Random.Generators;

namespace AIGames.Warlight2.Simulation
{
	/// <summary>Simulates attacks.</summary>
	public class Simulator
	{
		/// <summary>Chance of killing by attack is 60.0%.</summary>
		public const Double AttackFactor = 0.6;
		/// <summary>Chance of defence by attack is 70.0%.</summary>
		public const Double DefendFactor = 0.7;

		/// <summary>Constructor.</summary>
		public Simulator(MT19937Generator rnd)
		{
			this.Rnd = Guard.NotNull(rnd, "rnd");
		}

		/// <summary>Gets the randomizer.</summary>
		public MT19937Generator Rnd { get; protected set; }


		public SimulationDistribution Simulate(RegionState source, RegionState target, int armies, int simulations = 1000)
		{
			if (source.Owner == target.Owner)
			{
				var outcome = new SimulationOutcome(source.Leave(armies), target.Arive(armies));
				return null;
			}

			var success = new Dictionary<RegionState, int>();
			var failure = new Dictionary<SimulationOutcome, int>();

			for (var simulation = 0; simulation < simulations; simulation++)
			{
				var outcome = SimulateAttack(source, target, armies);

				if (outcome.HaveSameOwner)
				{
					if (!success.ContainsKey(outcome.Target))
					{
						success[outcome.Target] = 1;
					}
					else
					{
						success[outcome.Target]++;
					}
				}
				else
				{
					if (!failure.ContainsKey(outcome))
					{
						failure[outcome] = 1;
					}
					else
					{
						failure[outcome]++;
					}
				}

			}
			return new SimulationDistribution()
			{
				Source = source.Leave(armies),
				Success = success,
				Failure = failure,
			};
		}

		//see wiki.warlight.net/index.php/Combat_Basics
		private SimulationOutcome SimulateAttack(RegionState source, RegionState target, int armies)
		{
			//int attackers, int defenders, out int attackersDestroyed, out int defendersDestroyed

			int attackingArmies = armies;
			int defendingArmies = target.Armies;

			var attackersDestroyed = 0;
			var defendersDestroyed = 0;

			for (int t = 1; t <= attackingArmies; t++) //calculate how much defending armies are destroyed
			{
				double rand = Rnd.NextDouble();

				//60% chance to destroy one defending army
				if (rand < AttackFactor)
				{
					defendersDestroyed++;
				}
			}
			for (int t = 1; t <= defendingArmies; t++) //calculate how much attacking armies are destroyed
			{
				double rand = Rnd.NextDouble();
				//70% chance to destroy one attacking army
				if (rand < DefendFactor)
				{
					attackersDestroyed++;
				}
			}

			if (attackersDestroyed >= attackingArmies)
			{
				if (defendersDestroyed >= defendingArmies)
				{
					defendersDestroyed = defendingArmies - 1;
				}
				attackersDestroyed = attackingArmies;
			}

			//process result of attack
			if (defendersDestroyed >= defendingArmies) //attack success
			{
				var survivors = attackingArmies - attackersDestroyed;
				return new SimulationOutcome(source.Leave(armies), RegionState.Create(survivors, source.Owner));
			}
			else //attack fail
			{
				return new SimulationOutcome(source.Leave(attackersDestroyed), target.Leave(defendersDestroyed));
			}
		}

	}
}
