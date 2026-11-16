using System;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmeNativeAdLifetime : MonoBehaviour
	{
		private Action _onDestroyed;

		public void Initialize(Action onDestroyed)
		{
		}

		private void OnDestroy()
		{
		}
	}
}
