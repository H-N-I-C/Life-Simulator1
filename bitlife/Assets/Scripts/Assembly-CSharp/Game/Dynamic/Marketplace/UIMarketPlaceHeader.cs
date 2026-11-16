using System;
using BitLife.UI.CandyObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dynamic.Marketplace
{
	public class UIMarketPlaceHeader : MonoBehaviour
	{
		[Serializable]
		public class OriginalShop : Shop
		{
			public GameObject phone;

			public GameObject phoneUs;

			public GameObject tablet;

			public GameObject tabletUs;

			protected override void OnShow()
			{
			}
		}

		[Serializable]
		public class Shop
		{
			public GameObject parent;

			public TextMeshProUGUI message;

			public void Show(bool show)
			{
			}

			public void SetMessage(string text)
			{
			}

			protected virtual void OnShow()
			{
			}
		}

		private Image background;

		private GameObject tabParent;

		private CandyButtonToggle webShopTab;

		private CandyButtonToggle platformTab;

		private GameObject tabParentBanner;

		private CandyButtonToggle webShopTabBanner;

		private CandyButtonToggle platformTabBanner;

		private OriginalShop original;

		private Shop webShop;

		private Shop platform;

		private const string AppStoreMessage = "<b><size=+14>Higher Prices</size></b>\nPayments processed via the {0} Store";

		private static Action<UIShopType> m_NotifyShopTypeTabClicked;

		private static UIShopType ShopType__BackingField;

		private static bool ShowBanner__BackingField;

		public static UIShopType ShopType
		{
			get
			{
				return ShopType__BackingField;
			}
			private set
			{
				ShopType__BackingField = value;
			}
		}

		public static bool ShowBanner
		{
			get
			{
				return ShowBanner__BackingField;
			}
			private set
			{
				ShowBanner__BackingField = value;
			}
		}

		private static event Action<UIShopType> NotifyShopTypeTabClicked
		{
			add
			{
			}
			remove
			{
			}
		}

		public static UIShopType GetShopType(Action<UIShopType> notifyShopTypeTabClicked)
		{
			return UIShopType.Original;
		}

		public static void RemoveListener(Action<UIShopType> notifyShopTypeTabClicked)
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnWebShopTab(bool isOn)
		{
		}

		private void OnPlatformTab(bool isOn)
		{
		}

		private void SetShopType(UIShopType shopType, bool showBanner, bool forceRefresh = false, bool notifyClicked = false)
		{
		}

		private void SetupTabs(UIShopType shopType, bool showBanner)
		{
		}

		private void RefreshHeight(float height)
		{
		}
	}
}
