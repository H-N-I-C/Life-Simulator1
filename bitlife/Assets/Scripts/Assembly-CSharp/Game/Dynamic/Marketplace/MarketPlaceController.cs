using System;
using System.Collections.Generic;
using Game.Dynamic.Marketplace.Rules;

namespace Game.Dynamic.Marketplace
{
	public class MarketPlaceController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<MarketPlaceEntry, bool> _003C_003E9__20_1;

			internal bool _003CLoadID_003Eb__20_1(MarketPlaceEntry x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass15_0
		{
			public MARKTPLACE_TYPE category;

			internal bool _003CGetEntriesForCategory_003Eb__0(MarketPlaceEntry x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public int iapType;

			internal bool _003CGetEntryForIapType_003Eb__0(MarketPlaceEntry x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public string ID;

			public string appVersion;

			internal bool _003CLoadID_003Eb__0(MarketPlaceModel x)
			{
				return false;
			}
		}

		private readonly List<IMarketplaceListRule> _rules;

		private readonly List<IAP_TYPE> _bundleTypes;

		public static Action OnTimeUpdate;

		private MarketPlaceModel _model;

		private string _currentId;

		private List<MarketPlaceModel> _availableModels;

		public MarketPlaceModel Model => null;

		public List<MarketPlaceModel> Models => null;

		public static string GetPrice(IAP_ORIGIN exPackType)
		{
			return null;
		}

		public static string GetPrice(EXPANSION_PACK_TYPE exPackType)
		{
			return null;
		}

		~MarketPlaceController()
		{
		}

		private void ReportTime()
		{
		}

		public List<MarketPlaceEntry> GetEntriesForCategory(MARKTPLACE_TYPE category)
		{
			return null;
		}

		public int GetColorForCategory(MARKTPLACE_TYPE listEntryValue)
		{
			return 0;
		}

		public int GetColorForCategoryLabel(MARKTPLACE_TYPE listEntryValue)
		{
			return 0;
		}

		public MarketPlaceEntry GetEntryForIapType(int iapType)
		{
			return null;
		}

		public void LoadContent(List<MarketPlaceModel> data)
		{
		}

		public void LoadID(string ID, string appVersion)
		{
		}

		private void ApplyOverrideRules()
		{
		}

		private void LoadDefault()
		{
		}

		private void SetSpecialItems(List<IAP_TYPE> iapTypes)
		{
		}

		public static (string, bool) GetUpdatedTimeString(int dataTimeInSeconds)
		{
			return default;
		}
	}
}
