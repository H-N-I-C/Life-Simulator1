using System;
using System.Collections.Generic;
using Gadsme;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public static class GadsmeNativeRowFactory
	{
		public static GameObject CreateRow(GadsmeNativeRequest rowRequest, Action onDestroyed)
		{
			return null;
		}

		private static bool HasExpectedPlacementCount(IReadOnlyList<GadsmePlacementBanner> placementSlots, int expectedCount, string rowPrefabResourcesPath, GameObject rowRoot)
		{
			return false;
		}

		private static void ConfigurePlacement(GadsmePlacementBanner placement, GadsmePlacementRequest placementRequest)
		{
		}
	}
}
