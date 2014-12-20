using System;

namespace AIGames.Warlight2
{
	/// <summary>Some input guard plating.</summary>
	public static class Guard
	{
		/// <summary>Guards for not null.</summary>
		public static T NotNull<T>(T obj, string name) where T : class
		{
#if DEBUG
			if (obj == null) { throw new ArgumentNullException(name); }
#endif
			return obj;
		}

		/// <summary>Guards for not null/zero length.</summary>
		public static T[] NotNullOrEmpty<T>(T[] array, string name)
		{
#if DEBUG
			if (array == null || array.Length == 0) { throw new ArgumentNullException(name); }
#endif
			return array;
		}
	}
}
