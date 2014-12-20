using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.UnitTests.TestData
{
	public class TestStream
	{
		public static Stream Get(string name)
		{
			return typeof(TestStream).Assembly.GetManifestResourceStream("AIGames.Warlight2.UnitTests.TestData." + name);
		}
	}
}
