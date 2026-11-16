using System;
using System.Collections.Generic;

namespace Genesis.FeatureContainers
{
	public static class GenesisFeatureContainerAgeUp
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Comparison<(int, Scenario)> _003C_003E9__0_0;

			internal int _003CProcessAgeUp_003Eb__0_0((int, Scenario) a, (int, Scenario) b)
			{
				return 0;
			}
		}

		public static List<Scenario> ProcessAgeUp(Life life)
		{
			return null;
		}

		public static void RunBuiltInBehaviors(GenesisFeatureContainer container, GenesisFeatureContainerDefinition definition, Life life)
		{
		}

		internal static void RefreshCachedDraws(GenesisFeatureContainer container, Life life)
		{
		}

		private static bool TrySplitCollectionFromKey(string key, out string collectionName)
		{
			collectionName = null;
			return false;
		}

		public static bool ShouldRefreshCachedDraw(string refreshKind, int drawAge, int currentHeroAge)
		{
			return false;
		}

		public static void ResetAgeUpProperties(GenesisFeatureContainer container, List<(string, string, string)> resets)
		{
		}

		internal static void AgeCollectionEntities(GenesisFeatureContainer container, Life life)
		{
		}

		internal static void CleanupDeadEntities(GenesisFeatureContainer container, Life life)
		{
		}

		private static Scenario LoadAgeUpScenario(GenesisFeatureContainer container, GenesisFeatureContainerDefinition definition, Life life)
		{
			return null;
		}

		public static int GetPriorityOrder(string priority)
		{
			return 0;
		}
	}
}
