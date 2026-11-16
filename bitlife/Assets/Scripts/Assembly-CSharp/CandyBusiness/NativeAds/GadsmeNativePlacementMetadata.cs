using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmeNativePlacementMetadata : MonoBehaviour
	{
		private string InternalPlacementName__BackingField;

		public string InternalPlacementName
		{
			get
			{
				return InternalPlacementName__BackingField;
			}
			private set
			{
				InternalPlacementName__BackingField = value;
			}
		}

		public void Initialize(string internalPlacementName)
		{
		}
	}
}
