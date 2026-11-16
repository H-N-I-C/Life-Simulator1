using System;
using System.Collections.Generic;
using Backend;
using BitLife.UI.CandyObjects;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UISeasonsTrackItem : MonoBehaviour
	{
		public enum ItemType
		{
			Free = 0,
			Premium = 1
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int> _003C_003E9__47_0;

			internal void _003COnClaimButtonClicked_003Eb__47_0(int index)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass47_0
		{
			public UISeasonsTrackItem _003C_003E4__this;

			public SeasonRedeemModel data;

			public Action<List<StuffItem>> _003C_003E9__2;

			internal void _003COnClaimButtonClicked_003Eb__1(bool success)
			{
			}

			internal void _003COnClaimButtonClicked_003Eb__2(List<StuffItem> myStuffResult)
			{
			}
		}

		private ItemType itemType;

		private CandyButton buttonItem;

		private CandyButton buttonClaim;

		private Image imageIcon;

		private TextMeshProUGUI textInfo;

		private Image imageCompleted;

		private Image imageLocked;

		private SeasonImageComponent imageContent;

		private SeasonImageComponent imageContentPressed;

		private string itemId;

		private int amount;

		private string redeemId;

		private MYSTUFF_CATEGORY category;

		private StuffItem item;

		private DataMyStuffCatalog myStuffData;

		private int index;

		private TrackItemModel itemData;

		private bool IsUnlockable__BackingField;

		private bool IsRedeemable__BackingField;

		private bool IsRedeemed__BackingField;

		private bool IsLocked__BackingField;

		private bool IsPremium__BackingField;

		public static Action<SeasonRedeemModel> OnItemRedeemed;

		private UIAlertView Alert => null;

		private bool HasBitPass => false;

		public bool IsUnlockable
		{
			get
			{
				return IsUnlockable__BackingField;
			}
			private set
			{
				IsUnlockable__BackingField = value;
			}
		}

		public bool IsRedeemable
		{
			get
			{
				return IsRedeemable__BackingField;
			}
			private set
			{
				IsRedeemable__BackingField = value;
			}
		}

		public bool IsRedeemed
		{
			get
			{
				return IsRedeemed__BackingField;
			}
			private set
			{
				IsRedeemed__BackingField = value;
			}
		}

		public bool IsLocked
		{
			get
			{
				return IsLocked__BackingField;
			}
			private set
			{
				IsLocked__BackingField = value;
			}
		}

		public bool IsPremium
		{
			get
			{
				return IsPremium__BackingField;
			}
			private set
			{
				IsPremium__BackingField = value;
			}
		}

		private void Awake()
		{
		}

		private void OnItemButtonClicked()
		{
		}

		private void ShowInfo<T>(T scenario) where T : ScenarioSeasonsTrackInfo
		{
		}

		public void SetData(int index, ScrollRect parentScroll, List<SeasonRedeemModel> redeemedItems, TrackItemModel itemData, ItemType itemType)
		{
		}

		private void OnClaimButtonClicked()
		{
		}

		private void ShowClaimInfo()
		{
		}

		private void Show(bool showComplete, bool showClaim, bool showLock)
		{
		}

		private void Show(bool showComplete, bool showClaim)
		{
		}

		private bool _003CSetData_003Eb__46_0(SeasonRedeemModel i)
		{
			return false;
		}

		private void _003CShowClaimInfo_003Eb__48_0(int index)
		{
		}
	}
}
