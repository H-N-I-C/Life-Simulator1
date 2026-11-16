using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Dynamic.Marketplace;

public class MainMenuManager
{
	public class ListItemTitleComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			return 0;
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_0;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_2;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_4;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_6;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_8;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_10;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_12;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_14;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_16;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_18;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__21_20;

		public static Func<string, int> _003C_003E9__31_0;

		public static Func<Func<bool>, bool> _003C_003E9__41_0;

		public static Func<SaveSlot, DateTime> _003C_003E9__58_0;

		public static Func<SaveSlot, DateTime> _003C_003E9__58_1;

		public static Func<SimArtifact, string> _003C_003E9__59_0;

		public static Func<SimModelGift, string> _003C_003E9__60_0;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_0;

		public static Func<SimAuctionItem, string> _003C_003E9__61_1;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_2;

		public static Func<SimAuctionItem, string> _003C_003E9__61_3;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_4;

		public static Func<SimAuctionItem, string> _003C_003E9__61_5;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_6;

		public static Func<SimAuctionItem, string> _003C_003E9__61_7;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_8;

		public static Func<SimAuctionItem, string> _003C_003E9__61_9;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_10;

		public static Func<SimAuctionItem, string> _003C_003E9__61_11;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_12;

		public static Func<SimAuctionItem, string> _003C_003E9__61_13;

		public static Func<SimAuctionItem, bool> _003C_003E9__61_14;

		public static Func<SimAuctionItem, string> _003C_003E9__61_15;

		public static Func<bool, bool> _003C_003E9__63_0;

		public static Func<bool, bool> _003C_003E9__63_1;

		public static Func<SaveSlot, DateTime> _003C_003E9__75_0;

		public static Action<int> _003C_003E9__75_1;

		public static Action<int> _003C_003E9__75_2;

		internal string _003CGetHolidayCollectionList_003Eb__21_0(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_2(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_4(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_6(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_8(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_10(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_12(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_14(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_16(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_18(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal string _003CGetHolidayCollectionList_003Eb__21_20(KeyValuePair<string, string> x)
		{
			return null;
		}

		internal int _003CGetCemeteryListWithSortType_003Eb__31_0(string x)
		{
			return 0;
		}

		internal bool _003CGetCount_003Eb__41_0(Func<bool> i)
		{
			return false;
		}

		internal DateTime _003CGetSavedLivesList_003Eb__58_0(SaveSlot t)
		{
			return default;
		}

		internal DateTime _003CGetSavedLivesList_003Eb__58_1(SaveSlot t)
		{
			return default;
		}

		internal string _003CGetArtifactsList_003Eb__59_0(SimArtifact t)
		{
			return null;
		}

		internal string _003CGetDesignerClothesList_003Eb__60_0(SimModelGift t)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_0(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_1(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_2(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_3(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_4(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_5(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_6(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_7(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_8(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_9(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_10(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_11(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_12(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_13(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetTreasureList_003Eb__61_14(SimAuctionItem t)
		{
			return false;
		}

		internal string _003CGetTreasureList_003Eb__61_15(SimAuctionItem x)
		{
			return null;
		}

		internal bool _003CGetChallengesList_003Eb__63_0(bool x)
		{
			return false;
		}

		internal bool _003CGetChallengesList_003Eb__63_1(bool x)
		{
			return false;
		}

		internal DateTime _003CShowTooManyLivesPrompt_003Eb__75_0(SaveSlot x)
		{
			return default;
		}

		internal void _003CShowTooManyLivesPrompt_003Eb__75_1(int index)
		{
		}

		internal void _003CShowTooManyLivesPrompt_003Eb__75_2(int index)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__1(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_1
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__3(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_10
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__21(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_2
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__5(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_3
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__7(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_4
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__9(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_5
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__11(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_6
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__13(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_7
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__15(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_8
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__17(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_9
	{
		public KeyValuePair<string, string> kvp;

		internal bool _003CGetHolidayCollectionList_003Eb__19(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	public const string CUSTOM_CITIES_TITLE = "Custom Cities";

	public const string CUSTOM_PEOPLE_TITLE = "Custom People";

	public const string CEMETERY_TITLE = "Cemetery";

	public const string SAVED_LIVES_TITLE = "Lives in Progress";

	public const string FAMILY_PLOT_SUB_STRING = "Family Plot";

	public const string LEADERBOARDS_TITLE = "Leaderboards";

	public const string LEADERBOARDS_HEADER_TITLE = "TODAY'S LEADERBOARDS";

	public const string EDIT_SPORTS_TEAMS_TITLE = "Edit Sports Teams";

	protected ArrayList leaderboardsHeroScoreIndexes;

	public ListItem LeaderboardsData;

	private static MainMenuManager sharedMainMenu;

	public bool EnteredMarketplaceFromInfoDarkly;

	public static MainMenuManager SharedMainMenuManager => null;

	private MonetizationVars MoneyVars => null;

	public List GetCommunityList()
	{
		return null;
	}

	public List GetMainMenuList()
	{
		return null;
	}

	private List GetPlaceholderList(string descriptionString, string headerTitleString)
	{
		return null;
	}

	public List GetLeaderboardsList()
	{
		return null;
	}

	public List GetAchievementList()
	{
		return null;
	}

	public List GetScavengerHuntList()
	{
		return null;
	}

	public List GetHolidayCollectionList(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return null;
	}

	private List GetWildLifeList()
	{
		return null;
	}

	public List GetRibbonsList()
	{
		return null;
	}

	public List GetEditSportsTeamsList()
	{
		return null;
	}

	public bool DoLeaderboardsHeroScoreIndexesInclude(int index)
	{
		return false;
	}

	public int GetNumberOfHeirloomsFound()
	{
		return 0;
	}

	public List GetLeaderboardListWithSortType(int sortType)
	{
		return null;
	}

	public void LoadLeaderboardsForData(ListItem data)
	{
	}

	public void LeaderboardScoresLoaded()
	{
	}

	public void LeaderboardScoresFailedToLoad()
	{
	}

	public List GetCemeteryListWithSortType(int pSortType, bool shouldRemoveBulldozedLegacies)
	{
		return null;
	}

	public List GetCustomPeopleList()
	{
		return null;
	}

	private List GetThemesList()
	{
		return null;
	}

	public List GetSettingsList()
	{
		return null;
	}

	private List GetMiniGameSettingList()
	{
		return null;
	}

	public List GetBitLifeMarketplaceList(bool fromInfoDarkly = false)
	{
		return null;
	}

	private IAP_TYPE GetTieredIAPReplacementItem(List<int> existingList)
	{
		return IAP_TYPE.NONE;
	}

	private bool DoesUserHaveAccessToFeaturedIAP(int featuredIap)
	{
		return false;
	}

	private int GetCount(Func<bool>[] conditions)
	{
		return 0;
	}

	private int GetTotalBundleOwnedPiecesZoo()
	{
		return 0;
	}

	private int GetTotalBundlePiecesOwned()
	{
		return 0;
	}

	private int GetTotalFameAndFortuneBundlePiecesOwned()
	{
		return 0;
	}

	private int GetTotalTimeIsMoneyItems()
	{
		return 0;
	}

	private int GetTotalCrimeOwned()
	{
		return 0;
	}

	private int GetCountLegends()
	{
		return 0;
	}

	private int GetBitizenshipGodModeCount()
	{
		return 0;
	}

	private int GetBirthdayLegendsCount()
	{
		return 0;
	}

	private int GetBundleStarter1Count()
	{
		return 0;
	}

	private int GetBundleStarter2Count()
	{
		return 0;
	}

	public bool TryGetSpecialListItem(MarketPlaceEntry entry, out ListItem item)
	{
		item = null;
		return false;
	}

	private bool TryGetSpecialListItem(MarketPlaceEntry entry, out ListItem item, bool metadataOnly)
	{
		item = null;
		return false;
	}

	private bool TryGetListItem(MarketPlaceEntry entry, out ListItem item)
	{
		item = null;
		return false;
	}

	public static bool TryGetMarketplaceProductMetadata(IAP_TYPE iapType, out ListItem item)
	{
		item = null;
		return false;
	}

	private static bool TryGetFeaturedListItem(IAP_TYPE iapType, out ListItem item, bool metadataOnly = false)
	{
		item = null;
		return false;
	}

	private List GetAccessoriesList()
	{
		return null;
	}

	public List GetSavedLivesList()
	{
		return null;
	}

	private List GetArtifactsList()
	{
		return null;
	}

	private List GetDesignerClothesList()
	{
		return null;
	}

	private List GetTreasureList()
	{
		return null;
	}

	private List GetCareersList()
	{
		return null;
	}

	public List GetChallengesList()
	{
		return null;
	}

	private List GetCompletedChallenges()
	{
		return null;
	}

	private List GetHeirloomsList()
	{
		return null;
	}

	public List GetItemsList()
	{
		return null;
	}

	public List GetCustomCitiesList()
	{
		return null;
	}

	private List GetSportsLeagueList(DataSportsLeague league)
	{
		return null;
	}

	private List GetFavoriteActivitiesList()
	{
		return null;
	}

	private List GetJobPacksList()
	{
		return null;
	}

	private List GetExpansionPacksList()
	{
		return null;
	}

	private ArrayList GetLeaderboardListItemsWithSortType(int sortType)
	{
		return null;
	}

	private List GetGenerationGravesFamilyNameList(ArrayList graves, string familyName)
	{
		return null;
	}

	public static bool ShowTooManyLivesPrompt(bool newLife)
	{
		return false;
	}

	internal static string _003CGetWildLifeList_003Eg__GetCountString_007C22_0(OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return null;
	}

	internal static string _003CGetWildLifeList_003Eg__GetEmoji_007C22_1(OUTDOOR_HANDBOOK_ENTRY_TYPE type)
	{
		return null;
	}

	private bool _003CGetTotalBundleOwnedPiecesZoo_003Eb__42_0()
	{
		return false;
	}

	private bool _003CGetTotalBundleOwnedPiecesZoo_003Eb__42_1()
	{
		return false;
	}

	private bool _003CGetTotalBundleOwnedPiecesZoo_003Eb__42_2()
	{
		return false;
	}

	private bool _003CGetTotalBundlePiecesOwned_003Eb__43_0()
	{
		return false;
	}

	private bool _003CGetTotalBundlePiecesOwned_003Eb__43_1()
	{
		return false;
	}

	private bool _003CGetTotalBundlePiecesOwned_003Eb__43_2()
	{
		return false;
	}

	private bool _003CGetTotalFameAndFortuneBundlePiecesOwned_003Eb__44_0()
	{
		return false;
	}

	private bool _003CGetTotalFameAndFortuneBundlePiecesOwned_003Eb__44_1()
	{
		return false;
	}

	private bool _003CGetTotalFameAndFortuneBundlePiecesOwned_003Eb__44_2()
	{
		return false;
	}

	private bool _003CGetTotalTimeIsMoneyItems_003Eb__45_0()
	{
		return false;
	}

	private bool _003CGetTotalTimeIsMoneyItems_003Eb__45_1()
	{
		return false;
	}

	private bool _003CGetTotalCrimeOwned_003Eb__46_0()
	{
		return false;
	}

	private bool _003CGetTotalCrimeOwned_003Eb__46_1()
	{
		return false;
	}

	private bool _003CGetTotalCrimeOwned_003Eb__46_2()
	{
		return false;
	}

	private bool _003CGetCountLegends_003Eb__47_0()
	{
		return false;
	}

	private bool _003CGetCountLegends_003Eb__47_1()
	{
		return false;
	}

	private bool _003CGetCountLegends_003Eb__47_2()
	{
		return false;
	}

	private bool _003CGetBitizenshipGodModeCount_003Eb__48_0()
	{
		return false;
	}

	private bool _003CGetBitizenshipGodModeCount_003Eb__48_1()
	{
		return false;
	}

	private bool _003CGetBirthdayLegendsCount_003Eb__49_0()
	{
		return false;
	}

	private bool _003CGetBirthdayLegendsCount_003Eb__49_1()
	{
		return false;
	}

	private bool _003CGetBundleStarter1Count_003Eb__50_0()
	{
		return false;
	}

	private bool _003CGetBundleStarter1Count_003Eb__50_1()
	{
		return false;
	}

	private bool _003CGetBundleStarter1Count_003Eb__50_2()
	{
		return false;
	}

	private bool _003CGetBundleStarter2Count_003Eb__51_0()
	{
		return false;
	}

	private bool _003CGetBundleStarter2Count_003Eb__51_1()
	{
		return false;
	}

	private bool _003CGetBundleStarter2Count_003Eb__51_2()
	{
		return false;
	}

	private bool _003CGetBundleStarter2Count_003Eb__51_3()
	{
		return false;
	}

	private bool _003CGetBundleStarter2Count_003Eb__51_4()
	{
		return false;
	}
}
