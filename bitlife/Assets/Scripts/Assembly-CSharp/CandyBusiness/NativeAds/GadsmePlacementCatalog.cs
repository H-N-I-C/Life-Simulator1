using System;
using System.Collections.Generic;
using UnityEngine;

namespace CandyBusiness.NativeAds
{
	public static class GadsmePlacementCatalog
	{
		private const string DefaultAdType = "mrec_1";

		private const string MediumRectangleAdType = "mrec_1";

		private const string DoubleMediumRectangleAdType = "mrec_2";

		private const int MediumRectangleRowCount = 5;

		private const int DoubleMediumRectangleRowCount = 5;

		private const string MediumRectangleRowPrefabResourcesPath = "prefabs/Gadsme/Rows/Medium Rectangle Row";

		private const string DoubleMediumRectangleRowPrefabResourcesPath = "prefabs/Gadsme/Rows/Double Medium Rectangle Row";

		private static readonly Vector2 MediumRectangleSize;

		private static readonly IReadOnlyList<GadsmeNativeRequest> MediumRectangleRowRequests;

		private static readonly IReadOnlyList<GadsmeNativeRequest> DoubleMediumRectangleRowRequests;

		public static IReadOnlyList<GadsmeNativeRequest> GetRowRequests(NativeAdsDataSO adTypeData)
		{
			return null;
		}

		public static bool HasAnyRowRequests()
		{
			return false;
		}

		private static string ResolveAdType(NativeAdsDataSO adTypeData)
		{
			return null;
		}

		private static GadsmeNativeRequest CreateSingleMediumRectangleRowRequest(int rowNumber)
		{
			return null;
		}

		private static GadsmeNativeRequest CreateDoubleMediumRectangleRowRequest(int rowNumber)
		{
			return null;
		}

		private static IReadOnlyList<GadsmeNativeRequest> CreateMediumRectangleRowRequests(int rowCount)
		{
			return null;
		}

		private static IReadOnlyList<GadsmeNativeRequest> CreateDoubleMediumRectangleRowRequests(int rowCount)
		{
			return null;
		}

		private static IReadOnlyList<GadsmeNativeRequest> CreateRowRequests(int rowCount, Func<int, GadsmeNativeRequest> factory)
		{
			return null;
		}

		private static bool HasAnyRowRequests(IReadOnlyList<GadsmeNativeRequest> rowRequests)
		{
			return false;
		}
	}
}
