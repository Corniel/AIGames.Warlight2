using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Troschuetz.Random.Generators;

namespace AIGames.Warlight2.Game
{
	/// <summary>Handles the combat ruling.</summary>
	public static class Combat
	{
		/// <summary>Making the lookup thread-safe.</summary>
		private static readonly object locker = new object();

		/// <summary>Contains the calculated chances.</summary>
		private static readonly Dictionary<int, double> winningChanceLookup = new Dictionary<int, double>();

		/// <summary>Chance of killing by attack is 60.0%.</summary>
		public const Double AttackFactor = 0.6;
		/// <summary>Chance of defence by attack is 70.0%.</summary>
		public const Double DefendFactor = 0.7;

		/// <summary>Gets the winning chance for the attacker.</summary>
		public static Double GetWinningChance(Int32 attackers, Int32 defenders)
		{
			if (attackers < 1) { return 0.0; }

			Double chance;
			lock (locker)
			{
				int key = attackers + (defenders << 16);
				if (!winningChanceLookup.TryGetValue(key, out chance))
				{
					chance = CalculateWinningChance(attackers, defenders);
					winningChanceLookup[key] = chance;
				}
			}
			return chance;
		}
		/// <summary>Applies an average attack.</summary>
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "out params have the smallest overhead in this case.")]
		public static bool ApplyAverageAttack(int attackers, int defenders, out int sourceLeave, out int defendersNew)
		{
			unchecked
			{
				var attDestroyed = (6 + defenders * 7) / 10;
				var defDestroyed = (5 + attackers * 6) / 10;

				if (defDestroyed >= defenders)
				{
					sourceLeave = attackers;
					defendersNew = attackers - attDestroyed;
					return true;
				}
				else
				{
					sourceLeave = Math.Min(attackers, attDestroyed);
					defendersNew = defenders - defDestroyed;
					return false;
				}
			}
		}
		//see wiki.warlight.net/index.php/Combat_Basics
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "out params have the smallest overhead in this case.")]
		public static bool SimulateAttack(int attackers, int defenders, MT19937Generator rnd, out int attackersDestroyed, out int defendersDestroyed)
		{
			int attackingArmies = attackers;
			int defendingArmies = defenders;

			attackersDestroyed = 0;
			defendersDestroyed = 0;

			for (int t = 1; t <= attackingArmies; t++) //calculate how much defending armies are destroyed
			{
				double rand = rnd.NextDouble();

				//60% chance to destroy one defending army
				if (rand < AttackFactor)
				{
					defendersDestroyed++;
				}
			}
			for (int t = 1; t <= defendingArmies; t++) //calculate how much attacking armies are destroyed
			{
				double rand = rnd.NextDouble();
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
				return true;
			}
			else //attack fail
			{
				return false;
			}
		}

		/// <summary>Calculates the winning chance.</summary>
		/// <remarks>
		/// 1 attacker, 1 defender.
		/// 
		///       defender
		/// a     |  0  |  1
		/// t  ===============
		/// t   0 | .12 | .18
		/// a  ---------------
		/// c   1 | .28 | .42 
		/// k  ---------------
		/// 
		/// winning chance = .18
		/// </remarks>
		private static Double CalculateWinningChance(Int32 attackers, Int32 defenders)
		{
			if (defenders > attackers) { return 0.0; }

			var chanceForKilledDefenders = 0.0;

			// We need to kill at least all defenders (more is OK).
			for (int killedDefenders = defenders; killedDefenders <= attackers; killedDefenders++)
			{
				chanceForKilledDefenders += GetChance(attackers, killedDefenders, AttackFactor);
			}

			// if they are equal we have the chance to kill all attacker too.
			if (defenders == attackers)
			{
				var chanceForKilledAttackers = 1.0 - GetChance(attackers, attackers, DefendFactor);
				var chance = chanceForKilledDefenders * chanceForKilledAttackers;
				return chance;
			}
			else
			{
				return chanceForKilledDefenders;
			}
		}
		private static Double GetChance(int tries, int goal, double chance)
		{
			var score = Math.Pow(chance, goal) * Math.Pow(1 - chance, tries - goal);

			if (score > 0.0)
			{
				score *= Permutate(tries, goal);
			}
			return score;
		}
		private static Double Permutate(int n, int k)
		{
			int nk = n - k;
			nk = Math.Min(nk, n - nk);
			Double score = 1.0;

			for (int i = 0; i < nk; i++)
			{
				score *= n - i;
				score /= nk - i;
			}
			return score;
		}
	}
}
