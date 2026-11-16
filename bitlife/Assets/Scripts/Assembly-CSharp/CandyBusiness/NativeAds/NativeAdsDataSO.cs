using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public class NativeAdsDataSO : ScriptableObject
	{
		public string AdType;

		public float AspectRatio;

		public float DefaultRowHeight;

		public int ItemsPerRow;

		public float Spacing;

		public int WidthAspectRatio;

		public int HeightAspectRatio;

		public float GetAspectRatio(int rowHeight)
		{
			return 0f;
		}
	}
}
