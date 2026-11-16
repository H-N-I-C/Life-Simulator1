using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HolidayCollectionManager : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass13_0
	{
		public string collectible;

		internal bool _003CHasUserFoundCollectible_003Eb__0(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass15_0
	{
		public HOLIDAY_COLLECTIBLE_TYPE type;

		internal bool _003CNumItemsCompletedForHolidayType_003Eb__0(HolidayCollectibleMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass45_0
	{
		public string collectibleTitle;

		internal bool _003CGetCollectibleItemForTitle_003Eb__0(HolidayCollectibleMetadata item)
		{
			return false;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass49_0
	{
		public bool activeOnly;

		public HolidayCollectionManager _003C_003E4__this;

		public Action<string, string, HOLIDAY_COLLECTIBLE_TYPE, string, int, int> addItem;

		public int count;
	}

	public static HolidayCollectionManager SharedHolidayCollectionManager;

	private readonly Dictionary<string, string> cachedTotemHintStrings;

	public static readonly Dictionary<HOLIDAY_COLLECTIBLE_TYPE, string> ScavengerHuntTitles;

	public static readonly Dictionary<HOLIDAY_COLLECTIBLE_TYPE, string> ScavengerHuntIds;

	private List<HolidayCollectibleMetadata> CollectedHolidayItems__BackingField;

	public List<HolidayCollectibleMetadata> CollectedHolidayItems
	{
		get
		{
			return CollectedHolidayItems__BackingField;
		}
		private set
		{
			CollectedHolidayItems__BackingField = value;
		}
	}

	public static bool ScavengerHuntIdMatches(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return false;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void SaveHolidayCollectible()
	{
	}

	public void LoadHolidayCollectibles()
	{
	}

	public bool HasUserFoundCollectible(string collectible)
	{
		return false;
	}

	public void FindCollectible(HolidayCollectibleMetadata metadata)
	{
	}

	public int NumItemsCompletedForHolidayType(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return 0;
	}

	public Dictionary<string, string> GetNames(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return null;
	}

	public Dictionary<string, string> GetScavengerHuntNames<T>() where T : ScavengerHunt, new()
	{
		return null;
	}

	public Dictionary<string, string> EasterEggHuntNames()
	{
		return null;
	}

	public Dictionary<string, string> PrideHuntNames()
	{
		return null;
	}

	public Dictionary<string, string> DeadlyHuntNames()
	{
		return null;
	}

	public Dictionary<string, string> EasterZodiacHuntNames()
	{
		return null;
	}

	public Dictionary<string, string> Halloween2024HuntNames()
	{
		return null;
	}

	public Dictionary<string, string> ThanksGiving2024HuntNames()
	{
		return null;
	}

	public Dictionary<string, string> LunarNewYear2025Names()
	{
		return null;
	}

	public Dictionary<string, string> GetValentine2025Names()
	{
		return null;
	}

	public int NumItemsForHolidayType(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return 0;
	}

	public int NumScavengerHunts()
	{
		return 0;
	}

	public int NumScavengersCompleted()
	{
		return 0;
	}

	public bool IsScavengerHuntAvailable()
	{
		return false;
	}

	public bool IsActive(HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return false;
	}

	public bool IsPride2024Month()
	{
		return false;
	}

	public bool IsHalloween2024()
	{
		return false;
	}

	public bool IsThanksGiving2024()
	{
		return false;
	}

	public bool IsLunarNewYear2025()
	{
		return false;
	}

	public bool IsValentine2025()
	{
		return false;
	}

	public bool IsDeadlySins2024()
	{
		return false;
	}

	public bool IsZodiac2024()
	{
		return false;
	}

	public bool IsEaster2024Season()
	{
		return false;
	}

	public void AnnualScavengerHuntCheck()
	{
	}

	private void AnnualBirthdayChecks()
	{
	}

	private void AnnualZodiacBankBalanceCheck()
	{
	}

	public string GetHintForTotem(string totem)
	{
		return null;
	}

	private Dictionary<string, string> GetHintForTotem<T>() where T : ScavengerHunt, new()
	{
		return null;
	}

	public HOLIDAY_COLLECTIBLE_TYPE GetScavengerHuntForTotem(string totemName)
	{
		return HOLIDAY_COLLECTIBLE_TYPE.NONE;
	}

	public HolidayCollectibleMetadata GetCollectibleItemForTitle(string collectibleTitle)
	{
		return null;
	}

	public Scenario CollectHolidayItem(string item, HOLIDAY_COLLECTIBLE_TYPE type)
	{
		return null;
	}

	public string GetRedeemCode(HolidayCollectibleMetadata collectible)
	{
		return null;
	}

	public bool IsCompleted(HOLIDAY_COLLECTIBLE_TYPE item)
	{
		return false;
	}

	public bool TryGetScavengerHuntList(bool activeOnly, Action<string, string, HOLIDAY_COLLECTIBLE_TYPE, string, int, int> addItem)
	{
		return false;
	}

	private void _003CTryGetScavengerHuntList_003Eg__AddItem_007C49_0(string name, string emoji, HOLIDAY_COLLECTIBLE_TYPE type, string secondLine, ref _003C_003Ec__DisplayClass49_0 P_4)
	{
	}
}
