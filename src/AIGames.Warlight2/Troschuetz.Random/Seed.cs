using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGames.Warlight2.Troschuetz.Random
{
	public static class Seed
	{
		public static int Generate(IEnumerable<Int32> ids)
		{
			var seed = 0;
			unchecked
			{
				int shift = 0;
				int step = Math.Max(1, 32 / ids.Count() + 1);

				foreach (var id in ids)
				{
					seed ^= (id << shift);
					shift += step;
				}
			}
			return seed;
		}
	}
}
