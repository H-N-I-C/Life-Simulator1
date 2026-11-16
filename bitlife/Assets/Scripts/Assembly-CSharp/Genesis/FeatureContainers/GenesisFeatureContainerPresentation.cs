using System;

namespace Genesis.FeatureContainers
{
	public static class GenesisFeatureContainerPresentation
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Comparison<GenesisFeatureContainerDefinition> _003C_003E9__1_0;

			internal int _003CTryGetStringOverride_003Eb__1_0(GenesisFeatureContainerDefinition left, GenesisFeatureContainerDefinition right)
			{
				return 0;
			}
		}

		public const string HeroOccupationOverrideKey = "hero_occupation_override";

		public static bool TryGetStringOverride(Life life, string propertyKey, out string value)
		{
			value = null;
			return false;
		}
	}
}
