using System;
using System.Collections.Generic;
using Gadsme;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmeNativeAdsLoader : IDisposable
	{
		private readonly Action<INativeAd> _onLoadComplete;

		private readonly bool _retryLoad;

		private readonly GameObject _rowRoot;

		private readonly HashSet<GadsmePlacement> _trackedPlacements;

		private readonly HashSet<GadsmePlacement> _pendingPlacements;

		private int _progressTimerValue;

		private int _retryAttempt;

		private bool _isPublished;

		private string _rowPrefabPath;

		private bool HasReceivedSdkCallback__BackingField;

		private static string LastLoadedPlacementName__BackingField;

		private static readonly HashSet<string> LivePlacementNames;

		private readonly List<string> _livePlacementNames;

		private bool _isDisposed;

		private float _retryDueAtRealtime;

		private bool _failureHandledThisAttempt;

		public bool IsPublished => false;

		public bool HasReceivedSdkCallback
		{
			get
			{
				return HasReceivedSdkCallback__BackingField;
			}
			private set
			{
				HasReceivedSdkCallback__BackingField = value;
			}
		}

		internal static string LastLoadedPlacementName
		{
			get
			{
				return LastLoadedPlacementName__BackingField;
			}
			private set
			{
				LastLoadedPlacementName__BackingField = value;
			}
		}

		public bool IsRetryDue => false;

		internal static string GetImpressionPlacementNameOrNull()
		{
			return null;
		}

		internal static void ResetImpressionAttributionState()
		{
		}

		public GadsmeNativeAdsLoader(Action<INativeAd> onLoadComplete, GadsmeNativeRequest rowRequest, bool retryLoad, bool debugFail = false)
		{
		}

		public void Dispose()
		{
		}

		public void Destroy()
		{
		}

		private bool IsTrackedPlacement(GadsmePlacement placement)
		{
			return false;
		}

		private void HandlePlacementLoaded(GadsmePlacement placement)
		{
		}

		private void HandlePlacementClicked(GadsmePlacement placement)
		{
		}

		private void HandlePlacementFailed(GadsmePlacement placement)
		{
		}

		private void HandlePlacementNoAd(GadsmePlacement placement)
		{
		}

		private void HandleAdContentFailed(GadsmeAdContentInfo failedContent)
		{
		}

		private void HandleTrackedFailure()
		{
		}

		public void RetryNow()
		{
		}

		private string GetPlacementName(GadsmePlacement placement)
		{
			return null;
		}

		private IEnumerable<string> GetTrackedPlacementNames()
		{
			return null;
		}
	}
}
