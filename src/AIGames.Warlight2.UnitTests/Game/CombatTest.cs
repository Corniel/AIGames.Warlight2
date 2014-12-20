using AIGames.Warlight2.Game;
using NUnit.Framework;
using System;
using System.Linq;
using Troschuetz.Random.Generators;

namespace AIGames.Warlight2.UnitTests.Game
{
	[TestFixture]
	public class CombatTest
	{
		[Test]
		public void SimulateAttack_01vs10_AreEqual()
		{
			var runs = 100;
			var rnd = new MT19937Generator(17);

			var actAtt = new double[runs];
			var actDef = new double[runs];
			var actRes = new bool[runs];
			for (var i = 0; i < runs; i++)
			{
				int att;
				int def;

				actRes[i] = Combat.SimulateAttack(1, 10, rnd, out att, out def);
				actAtt[i] = (double)att;
				actDef[i] = (double)def;
			}

			Assert.AreEqual(0, actRes.Count(item => item), "results");
			Assert.AreEqual(1.00, actAtt.Average(), 0.01, "attackers");
			Assert.AreEqual(0.54, actDef.Average(), 0.01, "defenders");
		}

		[Test]
		public void SimulateAttack_02vs02_AreEqual()
		{
			var runs = 100;
			var rnd = new MT19937Generator(17);

			var actAtt = new double[runs];
			var actDef = new double[runs];
			var actRes = new bool[runs];
			for (var i = 0; i < runs; i++)
			{
				int att;
				int def;

				actRes[i] = Combat.SimulateAttack(2, 2, rnd, out att, out def);
				actAtt[i] = (double)att;
				actDef[i] = (double)def;
			}

			Assert.AreEqual(19, actRes.Count(item => item), "results");
			Assert.AreEqual(1.40, actAtt.Average(), 0.01, "attackers");
			Assert.AreEqual(1.02, actDef.Average(), 0.01, "defenders");
		}

		[Test]
		public void SimulateAttack_10Vs07_AreEqual()
		{
			var runs = 100000;
			var rnd = new MT19937Generator(17);

			var actAtt = new double[runs];
			var actDef = new double[runs];
			var actRes = new bool[runs];
			for(var i = 0; i<runs; i++)
			{
				int att;
				int def;

				actRes[i] = Combat.SimulateAttack(10, 7, rnd, out att, out def);
				actAtt[i] = (double)att;
				actDef[i] = (double)def;
			}

			Assert.AreEqual(38039, actRes.Count(item => item), "results");
			Assert.AreEqual(4.900, actAtt.Average(),0.01, "attackers");
			Assert.AreEqual(5.990, actDef.Average(),0.01, "defenders");
		}

		[Test]
		public void ApplyAverageAttack_05To01_AreEqual()
		{
			int sourceLeave;
			int targetNew;

			var act = Combat.ApplyAverageAttack(5, 1, out sourceLeave, out targetNew);

			Assert.AreEqual(true, act);
			Assert.AreEqual(5, sourceLeave);
			Assert.AreEqual(4, targetNew);
		}
		[Test]
		public void ApplyAverageAttack_10To10_AreEqual()
		{
			int sourceLeave;
			int targetNew;

			var act = Combat.ApplyAverageAttack(10, 10, out sourceLeave, out targetNew);

			Assert.AreEqual(false, act);
			Assert.AreEqual(7, sourceLeave);
			Assert.AreEqual(4, targetNew);
		}
		[Test]
		public void ApplyAverageAttack_22To10_AreEqual()
		{
			int sourceLeave;
			int targetNew;

			var act = Combat.ApplyAverageAttack(21, 10, out sourceLeave, out targetNew);

			Assert.AreEqual(true, act);
			Assert.AreEqual(21, sourceLeave);
			Assert.AreEqual(14, targetNew);
		}


		[Test]
		public void ApplyAverageAttack_10To40_AreEqual()
		{
			int sourceLeave;
			int targetNew;

			var act = Combat.ApplyAverageAttack(10, 40, out sourceLeave, out targetNew);

			Assert.AreEqual(false, act);
			Assert.AreEqual(10, sourceLeave);
			Assert.AreEqual(34, targetNew);
		}
	}
}
