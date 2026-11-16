using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmePlacementRequest
	{
		private readonly string CustomPlacementName__BackingField;

		private readonly Vector2 AdPixelSize__BackingField;

		private readonly string GadsmePlacementId__BackingField;

		public string CustomPlacementName => CustomPlacementName__BackingField;

		public Vector2 AdPixelSize => AdPixelSize__BackingField;

		public string GadsmePlacementId => GadsmePlacementId__BackingField;

		public GadsmePlacementRequest(string customPlacementName, Vector2 adPixelSize, string gadsmePlacementId = null)
		{
		}
	}
}
