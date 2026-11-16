using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public interface INativeAd
	{
		void SetParent(GameObject parent);

		void SetAnchorMax(Vector2 anchorMax);

		void SetAnchorMin(Vector2 anchorMin);

		void SetPivot(Vector2 pivot);

		void SetAnchoredPosition3D(Vector3 position);

		void SetLocalScale(float scale);

		void ShowAd();

		void DestroyAd();
	}
}
