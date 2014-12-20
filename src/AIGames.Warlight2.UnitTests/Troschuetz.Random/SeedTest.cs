using AIGames.Warlight2.Troschuetz.Random;
using NUnit.Framework;

namespace Troschuetz.Random.UnitTests
{
	[TestFixture]
	public class SeedTest
	{
		[Test]
		public void Generate_SomeInts_AreEqual()
		{
			var act = Seed.Generate(new int[]{ 2, 3, 5, 7, 9, 12, 15 });
			var exp = -661416862;

			Assert.AreEqual(exp, act);
		}
	}
}
