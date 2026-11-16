using System;
using System.Collections.Generic;

namespace Genesis.FeatureContainers
{
	public static class GenesisDrawHelper
	{
		public static List<string> DrawWithoutReplacement(IReadOnlyList<string> pool, int count, Random rng, Func<string, float> weight = null)
		{
			return null;
		}

		public static List<string> DrawWithoutReplacement(IReadOnlyList<string> pool, int count, Random rng, Func<string, bool> filter, Func<string, float> weight)
		{
			return null;
		}

		private static int SelectIndex(List<string> items, Random rng, Func<string, float> weight)
		{
			return 0;
		}
	}
}
