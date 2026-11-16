using System;

namespace Genesis.FeatureContainers
{
	public static class GenesisFeatureContainerEntitlement
	{
		public static Func<string, bool> OwnershipCheckOverride;

		public static bool IsContainerAccessible(GenesisFeatureContainerDefinition def)
		{
			return false;
		}

		public static bool IsContainerAccessible(string containerId)
		{
			return false;
		}

		public static bool OwnsProduct(string productId)
		{
			return false;
		}
	}
}
