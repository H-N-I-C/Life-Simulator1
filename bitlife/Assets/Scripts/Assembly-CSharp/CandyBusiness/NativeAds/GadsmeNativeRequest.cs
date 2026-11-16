using System.Collections.Generic;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmeNativeRequest
	{
		private readonly string RowPrefabResourcesPath__BackingField;

		private readonly IReadOnlyList<GadsmePlacementRequest> PlacementRequests__BackingField;

		public string RowPrefabResourcesPath => RowPrefabResourcesPath__BackingField;

		public IReadOnlyList<GadsmePlacementRequest> PlacementRequests => PlacementRequests__BackingField;

		public GadsmeNativeRequest(string rowPrefabResourcesPath, IReadOnlyList<GadsmePlacementRequest> placementRequests)
		{
		}
	}
}
